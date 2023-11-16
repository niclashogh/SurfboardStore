using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lib.Services
{
    public class Rent : AbstractService
    {
        [Required, DisplayName("Start date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DisplayName("End date"), DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
