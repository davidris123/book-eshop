using Eshop.DomainEntities;
using Eshop.Repository.Interface;
using EshopWebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Order> _orders;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _orders = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return [.. _orders
                .Include(z => z.BooksInOrders)
                    .ThenInclude(z => z.Book)
                .Include(z => z.User)];
        }

        public Order GetDetailsForOrder(BaseEntity model)
        {
            return _orders
                .Include(z => z.BooksInOrders)
                    .ThenInclude(z => z.Book)
                .Include(z => z.User)
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
