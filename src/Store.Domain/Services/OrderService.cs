using System.Transactions;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Store.Domain.Utils;

namespace Store.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    // private readonly IAddressRepository _addressRepository;
    private readonly IProductRepository _productRepository;

    // private readonly ICouponRepository _couponRepository;
    private readonly IUserRepository _userRepository;
    
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderService(IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        // IAddressRepository addressRepository,
        IProductRepository productRepository,
        // ICouponRepository couponRepository,
        IUserRepository userRepository,
        IDateTimeProvider dateTimeProvider)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        // _addressRepository = addressRepository;
        _userRepository = userRepository;
        _dateTimeProvider = dateTimeProvider;
        _productRepository = productRepository;
        // _couponRepository = couponRepository;
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
            if (userResult.Value == null || userResult.Failure)
            {
                throw new InvalidOperationException("User doesn't exist");
            }

            var newOrder = new Order
            {
                Status = OrderStatus.Created,
                Subtotal = default,
                Description = order.Description,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = PaymentStatus.NotPaid,
                HasCoupon = order.HasCoupon,
                Discount = 0,
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
            if (order.HasCoupon)
            {
            }

            var orderItems = await CreateOrderItems(order, newOrder.Id);
            newOrder.OrderItems = orderItems;
            newOrder.Subtotal = orderItems.Sum(oi => oi.Quantity * oi.CurrentUnitPrice);
            var updateResult = await _orderRepository.UpdateOrderAsync(newOrder);
            updateResult.OnFailure(() =>
                throw new InvalidOperationException(
                    $"Problem updating order: {updateResult.Error}"));

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
            var serviceResult = await _productRepository.GetByIdAsync(itemDto.ProductId);
            if (serviceResult.Value == null || serviceResult.Failure)
            {
                throw new InvalidOperationException($"Product not found: {serviceResult.Error}");
            }

            var currUnitPrice = serviceResult.Value.Price;
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = itemDto.ProductId,
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

    public async Task UpdateOrderAsync(Order order)
    {
        var result = await _orderRepository.UpdateAsync(order);
        result.OnFailure(() => throw new Exception(result.Error));
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
    {
        var result = await _orderRepository.GetOrderItemsAsync(orderId);
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }
}