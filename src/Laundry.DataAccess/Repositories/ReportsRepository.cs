using Laundry.Domain.Contracts.Repositories;

namespace Laundry.DataAccess.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly LaundryDbContext _context;

    public ReportsRepository(LaundryDbContext context)
    {
        _context = context;
    }
}
