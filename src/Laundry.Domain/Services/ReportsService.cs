using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Laundry.Domain.Contracts.Services;
using System.IO;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IServiceService _serviceService;
    private readonly IOrderService _orderService;
    private readonly IDateTimeProvider _dateTime;
    
    public ReportsService(IServiceService serviceService, IOrderService orderService, IDateTimeProvider dateTime)
    {
        _serviceService = serviceService;
        _orderService = orderService;
        _dateTime = dateTime;
    }

    public async Task<MemoryStream> GetPriceListAsync()
    {
        var services = await _serviceService.GetAllAvailableServicesAsync();

        var memoryStream = new MemoryStream();
        var writer = new PdfWriter(memoryStream);
        writer.SetCloseStream(false);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("Список послуг"));

        foreach (var service in services)
        {
            document.Add(new Paragraph($"{service.Name} - {service.PricePerUnit:C} за {service.UnitType.ToString()}"));
        }

        document.Add(new Paragraph($"{_dateTime.Now.ToLongDateString()} " +
                                   $"{_dateTime.Now.ToLongTimeString()}"));

        document.Close();
        memoryStream.Position = 0;

        return memoryStream;
    }

    public async Task<MemoryStream> CreateChequeAsync(int orderId)
    {
        var order = await _orderService.GetOrderAsync(orderId);
        var memoryStream = new MemoryStream();
        var writer = new PdfWriter(memoryStream);
        writer.SetCloseStream(false);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);
        
        document.Add(new Paragraph("Чек"));
        document.Add(new Paragraph($"ID замовлення: {order.Id}"));
        
        foreach (var item in order.OrderItems)
        {
            document.Add(new Paragraph($"{item.Service.Name} - {item.Quantity} x {item.CurrentUnitPrice:C} грн."));
        }

        var discount = order.Discount ?? 0;
        var total = order.Subtotal - discount + order.DeliveryFee;
        document.Add(new Paragraph($"Знижка: {discount:C} грн."));
        document.Add(new Paragraph($"Доставка: {order.DeliveryFee:C} грн."));
        document.Add(new Paragraph($"Усього: {total:C} грн."));
        document.Add(new Paragraph($"Дата: {_dateTime.Now.ToLongDateString()} " +
                                   $"{_dateTime.Now.ToLongTimeString()}"));
        
        document.Close();
        memoryStream.Position = 0;
        
        return memoryStream;
    }
}
