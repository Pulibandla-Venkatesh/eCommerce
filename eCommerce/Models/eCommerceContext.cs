
using eCommerceDataLayer;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Models
{
    public class eCommerceContext : DbContext

    {
        public eCommerceContext(DbContextOptions<eCommerceContext> options)
        : base(options)
        {
        }

        public DbSet<CartModel> CartModel { get; set; } = null!;

        public DbSet<LoginModel> LoginModel { get; set; } = null!;

        public DbSet<OrderModel> OrderModel { get; set; } = null!;

        public DbSet<ProductModel> ProductModel { get; set; } = null!;

        public DbSet<UserModel> UserModel { get; set; } = null!;


    }
}
