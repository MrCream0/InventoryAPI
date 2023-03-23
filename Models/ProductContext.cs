using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }
    }
}
