using Eshop.DomainEntities;

namespace Eshop.Service.Interface
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetDetailsForOrder(BaseEntity model);
    }
}
