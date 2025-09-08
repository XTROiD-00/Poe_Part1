using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{

    public class Claim
    {
        public string FacultyName { get; set; }
        public string ModuleName { get; set; }
        public int Sessions { get; set; }
        public double Hours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public IFormFile Document { get; set; }
    }
}

