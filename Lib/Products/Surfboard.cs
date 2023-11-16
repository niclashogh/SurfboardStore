using System.ComponentModel.DataAnnotations;

namespace Lib.Products
{
    public class Surfboard : AbstractProduct
    {
        [Required]
        public double Length { get; set; }
        [Required]
        public double Width { get; set; }
        [Required]
        public double Thickness { get; set; }
        [Required]
        public double Volume { get; set; }
        [Required]
        public SurfboardType Type { get; set; }

        public string? Equipment { get; set; }
        public string? ImgUrl { get; set; }
    }
}
