using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Entities;

namespace Laundry.DataAccess.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(LaundryDbContext context) : base(context)
    {
    }
}