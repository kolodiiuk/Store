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

    public async Task<Result<IEnumerable<Feedback>>> GetFeedbacksForOrder(int orderId)
    {
        try
        {
            var feedbacks = await _context.Feedbacks
                .Where(f => f.OrderId == orderId).ToListAsync();
            if (feedbacks == null)
            {
                return Result<IEnumerable<Feedback>>.Fail<IEnumerable<Feedback>>($"No feedbacks");
            }

            return Result<IEnumerable<Feedback>>.Success((IEnumerable<Feedback>)feedbacks);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<Feedback>>.Fail<IEnumerable<Feedback>>(
                $"Error fetching feedbacks for order {orderId}: {e.Message}");
        }
    }
}