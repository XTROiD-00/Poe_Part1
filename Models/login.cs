using System.ComponentModel.DataAnnotations;

namespace Poe_Part1.Models
{
    public class login
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string surname { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
