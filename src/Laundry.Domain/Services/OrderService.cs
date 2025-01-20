using System.Diagnostics;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Laundry.Domain.Enums;
using Laundry.Domain.Utils;
using System.Transactions;

namespace Laundry.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IAddressRepository addressRepository,
        IServiceRepository serviceRepository,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _serviceRepository = serviceRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        var result = await _orderRepository.GetAllAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        var result = await _orderRepository.GetOrdersOfUser(userId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        var result = await _orderRepository.GetOrderAsync(orderId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<Order> PlaceOrderAsync(CreateOrderDto order)
    {
        try
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var userResult = await _userRepository.GetByIdAsync(order.UserId);
            userResult.OnFailure(() => 
                throw new InvalidOperationException("User doesn't exist"));

            var newOrder = new Order
            {
                Status = OrderStatus.Created,
                Subtotal = default,
                Description = order.Description,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = PaymentStatus.Paid,
                HasCoupon = false,
                DeliveryFee = order.DeliveryFee,
                AddressId = order.AddressId,
                UserId = order.UserId,
                CreatedAt = _dateTimeProvider.Now,
                OrderItems = new List<OrderItem>()
            };

            var result = await _orderRepository.CreateOrderAsync(newOrder);
            result.OnFailure(() => 
                throw new InvalidOperationException("Problem creating a new order record"));
            newOrder.Id = result.Value;

            var orderItems = await CreateOrderItems(order, newOrder.Id);
            newOrder.OrderItems = orderItems;
            newOrder.Subtotal = orderItems.Sum(oi => oi.Quantity * oi.CurrentUnitPrice);
            var updateResult = await _orderRepository.UpdateOrderAsync(newOrder);
            updateResult.OnFailure (() =>
                throw new InvalidOperationException($"Problem updating order: {updateResult.Error}"));

            transaction.Complete();
            
            return newOrder;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create order. Error: " + ex.Message);
        }
    }

    private async Task<List<OrderItem>> CreateOrderItems(CreateOrderDto order, int orderId)
    {
        var orderItems = new List<OrderItem>();
        foreach (var itemDto in order.OrderItems)
        {
            var serviceResult = await _serviceRepository.GetByIdAsync(itemDto.ServiceId);
            serviceResult.OnFailure(() => 
                throw new InvalidOperationException($"Service not found: {serviceResult.Error}"));

            var currUnitPrice = serviceResult.Value.PricePerUnit;
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ServiceId = itemDto.ServiceId,
                Quantity = itemDto.Quantity,
                CurrentUnitPrice = currUnitPrice,
                Total = itemDto.Quantity * currUnitPrice
            };

            var result = await _orderItemRepository.CreateAsync(orderItem);
            result.OnFailure(() =>
                throw new InvalidOperationException($"Failed to create order item: {result.Error}"));

            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var result = await _orderRepository.GetOrderAsync(orderId);
        result.OnFailure(() => throw new Exception(result.Error));
        result.Value.Status = status;
        await _orderRepository.UpdateOrderAsync(result.Value);
    }

    public Task UpdatePaymentStatusAsync(int orderId, PaymentStatus status)
    {
        throw new NotImplementedException();
    }
}