namespace Laundry.Domain.Contracts.Services;

public interface IReportsService
{
    Task<MemoryStream> GetPriceListAsync();
    Task<MemoryStream> CreateChequeAsync(int orderId);
}
