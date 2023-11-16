using Lib.Products;
using Lib.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Services
{
    public abstract class AbstractService
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public string? GuestEmail { get; set; }

        [Required]
        public int SurfboardId { get; set; }
        [Required, Column(TypeName ="decimal(18, 2)")]
        public decimal Price { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("GuestEmail")]
        public Guest Guest { get; set; }
        [ForeignKey("SurfboardId")]
        public Surfboard Surfboard { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

    }
}
