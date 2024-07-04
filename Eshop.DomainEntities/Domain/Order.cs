namespace Eshop.DomainEntities
{
    public class Order: BaseEntity
    {

        public Guid EshopApplicationUserId { get; set; }
        public virtual List<BooksInOrder>? BooksInOrders { get; set; }
    }
}
