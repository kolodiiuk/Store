using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Laundry.Domain.Contracts.Services;
using System.IO;

namespace Laundry.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IServiceService _serviceService;
    private readonly IOrderService _orderService;
    private readonly IEmailService _emailService;

    public ReportsService(IServiceService serviceService,
        IOrderService orderService, IEmailService emailService)
    {
        _serviceService = serviceService;
        _orderService = orderService;
        _emailService = emailService;
    }

    public async Task<MemoryStream> GetPriceListAsync()
    {
        var services = await _serviceService.GetAllAvailableServicesAsync();

        var memoryStream = new MemoryStream();
        var writer = new PdfWriter(memoryStream);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("Список послуг"));

        foreach (var service in services)
        {
            document.Add(new Paragraph($"{service.Name} - {service.PricePerUnit:C} за {service.UnitType}"));
        }

        document.Add(new Paragraph($"{DateTime.Now.ToLongDateString()} " +
                                   $"{DateTime.Now.ToLongTimeString()}"));

        document.Close();
        memoryStream.Position = 0;

        return memoryStream;
    }

    public async Task SendChequeWithEmail(int orderId, string email)
    {
        var order = await _orderService.GetOrderAsync(orderId);
        var memoryStream = new MemoryStream();
        var writer = new PdfWriter(memoryStream);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        document.Add(new Paragraph("Чек"));
        document.Add(new Paragraph($"ID замовлення: {order.Id}"));

        foreach (var item in order.OrderItems)
        {
            document.Add(new Paragraph($"{item.Service.Name} - {item.Quantity} x {item.CurrentUnitPrice:C} грн."));
        }

        document.Add(new Paragraph($"Знижка: {order.Discount:C} грн."));
        document.Add(new Paragraph($"Усього: {order.Subtotal:C} грн."));
        document.Add(new Paragraph($"Дата: {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}"));

        document.Close();
        memoryStream.Position = 0;

        await _emailService.SendEmailWithAttachment(email, "Чек", "", memoryStream, "cheque.pdf");
    }
}