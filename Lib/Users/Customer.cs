using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lib.Users
{
    public class Customer : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
