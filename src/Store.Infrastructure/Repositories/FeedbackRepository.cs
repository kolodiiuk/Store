using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.EntityFrameworkCore;

namespace Store.Infrastructure.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Result<IEnumerable<Feedback>>> GetFeedbacksForOrder(int orderId)
    {
        try
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.OrderId == orderId).ToListAsync();

            return Result.Success((IEnumerable<Feedback>)feedbacks);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<Feedback>>(
                $"Error fetching feedbacks for order {orderId}: {e.Message}");
        }
    }
}