using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Products
{
    public abstract class AbstractProduct
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int InStock { get; set; }
        [Required, Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
