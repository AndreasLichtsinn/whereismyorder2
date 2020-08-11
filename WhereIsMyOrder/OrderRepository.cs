using WhereIsMyOrder.Data;
using WhereIsMyOrder.Models;

namespace WhereIsMyOrder
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
