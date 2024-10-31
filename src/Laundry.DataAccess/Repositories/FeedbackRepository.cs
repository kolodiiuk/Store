using Laundry.DataAccess;
using Laundry.DataAccess.Repositories;
using Laundry.Domain.Entities;
using Laundry.Domain.Interfaces;

namespace Laundry.Api.DataAccess.Repositories;

public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(LaundryDbContext context) : base(context)
    {
        
    }
    // getByCond, add
}