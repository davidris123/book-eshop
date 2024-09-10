namespace Eshop.DomainEntities
{
    public class Order: BaseEntity
    {

        public string? EshopApplicationUserId { get; set; }
        public EshopApplicationUser User { get; set; }
        public virtual List<BooksInOrder>? BooksInOrders { get; set; }
    }
}
