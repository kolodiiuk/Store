using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Enums;
using Laundry.Domain.Utils;

namespace Laundry.DataAccess.Repositories;

//todo: think of removing
public class ReportsRepository : IReportsRepository
{
    private readonly LaundryDbContext _context;

    public ReportsRepository(LaundryDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Cheque>> GetChequeInfo(int orderId)
    {

    }
}

public class Cheque
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int OrderId { get; set; }
    public decimal Total { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal DeliveryFee { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
