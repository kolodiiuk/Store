namespace Laundry.Domain.Contracts.Services;

public interface IReportsService
{
    Task<MemoryStream> GetPriceListAsync();
    Task SendChequeWithEmail(int orderId, string email);
}
