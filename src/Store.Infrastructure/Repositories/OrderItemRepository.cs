using Store.Domain.Contracts.Repositories;
using Store.Domain.Entities;

namespace Store.Infrastructure.Repositories;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }
}