using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Claim
    {
        [Required]
        public string FacultyName { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public int Sessions { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public IFormFile Document { get; set; }
    }
}

/* using Microsoft.AspNetCore.Mvc;

namespace MVC_Models.Models
{
    public class Claim
    {
        public string Claimname
        { get; set; }
        public string Claimtype
        { get; set; }
        public string Claimdescription
        { get; set; }

    }
}

*/