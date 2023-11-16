using Microsoft.EntityFrameworkCore;
using Lib.Products;
using Lib.Services;

namespace Api.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Surfboard> Surfboard { get; set; } = default!;

        public DbSet<Rent>? Rent { get; set; }

        public DbSet<Buy>? Buy { get; set; }
    }
}
