namespace Laundry.Domain.Contracts.Services;

public interface IReportsService
{
    MemoryStream GetPriceList();
    void SendChequeWithEmail(int orderId);
}
