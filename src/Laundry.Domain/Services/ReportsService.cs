using iText.Kernel.Pdf;
using Laundry.Domain.Contracts.Services;
using iText.Layout;
using iText.Layout.Element;

namespace Laundry.Domain.Services;

public class ReportsService : IReportsService
{
    private readonly IServiceService _serviceService;

    public ReportsService(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public MemoryStream GetPriceList()
    {
        var services = _serviceService.GetAllAvailableServicesAsync().Result.ToList();

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

    public void SendChequeWithEmail(int orderId)
    {
        
    }
}
