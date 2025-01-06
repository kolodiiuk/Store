using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Laundry.DataAccess.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(LaundryDbContext context) : base(context)
    {
    }

    public Result<IQueryable<Feedback>> GetFeedbacksForOrder(int orderId)
    {
        try
        {
            var orderFeedbacks = _context.Feedbacks
                .Where(f => f.OrderId == orderId)
                .AsNoTracking();

            return Result.Success<IQueryable<Feedback>>(orderFeedbacks);
        }
        catch (Exception e)
        {
            return Result.Fail<IQueryable<Feedback>>(e.Message);
        }
    }
}