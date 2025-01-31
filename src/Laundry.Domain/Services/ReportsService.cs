using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Laundry.Domain.Contracts.Services;
using System.IO;
using iText.Kernel.Font;
using iText.Layout.Font;
using iText.Layout.Properties;
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

        document.Add(new Paragraph("Services"));

        foreach (var service in services)
        {
            document.Add(new Paragraph($"{service.Name} - {service.PricePerUnit:C} per {service.UnitType.ToString()}"));
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
        
        document.Add(new Paragraph("Cheque"));
        document.Add(new Paragraph($"Order ID: {order.Id}"));
        
        foreach (var item in order.OrderItems)
        {
            document.Add(new Paragraph($"{item.Service.Name} - {item.Quantity} x {item.CurrentUnitPrice:C} UAH"));
        }

        var discount = order.Discount ?? 0;
        var total = order.Subtotal - discount + order.DeliveryFee;
        document.Add(new Paragraph($"Discount: {discount} UAH"));
        document.Add(new Paragraph($"Delivery fee: {order.DeliveryFee} UAH"));
        document.Add(new Paragraph($"Total: {total} UAH"));
        document.Add(new Paragraph($"Date: {_dateTime.Now.ToLongDateString()} " +
                                   $"{_dateTime.Now.ToLongTimeString()}"));
        
        document.Close();
        memoryStream.Position = 0;
        
        return memoryStream;
    }
}
