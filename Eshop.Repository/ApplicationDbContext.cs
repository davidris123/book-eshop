using Eshop.DomainEntities;
using Eshop.DomainEntities.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EshopWebApplication1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<EshopApplicationUser>(options)
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BooksInShoppingCart> BooksInShoppingCarts { get; set; }

        public DbSet<BooksInOrder> BooksInOrders { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
        public DbSet<FoodPartner> FoodPartner { get; set; }
    }
}
