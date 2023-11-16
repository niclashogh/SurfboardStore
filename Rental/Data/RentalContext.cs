using Microsoft.EntityFrameworkCore;
using Lib.Products;

namespace Rental.Data
{
    public class RentalContext// : DbContext
    {
        //public RentalContext (DbContextOptions<RentalContext> options)
        //    : base(options)
        //{
        //}

        //public DbSet<Surfboard> Surfboard { get; set; }

        public IEnumerable<Surfboard> Surfboard { get; set; }
    }
}
