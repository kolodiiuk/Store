using Laundry.Domain.Entities;
using Laundry.Domain.Utils;

namespace Laundry.Domain.Contracts.Repositories;

public interface IFeedbackRepository : IGenericRepository<Feedback>
{
    Result<IQueryable<Feedback>> GetFeedbacksForOrder(int orderId);

}