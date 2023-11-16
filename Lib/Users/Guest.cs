using System.ComponentModel.DataAnnotations;

namespace Lib.Users
{
    public class Guest
    {
        public string Id { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
