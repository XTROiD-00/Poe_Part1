using System.ComponentModel.DataAnnotations;

namespace Poe_Part1.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }

        [Required]
        public string FacultyName { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public int Sessions { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public decimal Rate { get; set; }

        public decimal TotalAmount { get; set; }

        // If DocumentPath is also missing, add this property as well
        public string DocumentPath { get; set; }

        public string month => DateTime.Now.ToString("MMMM");
        public decimal totalAmount => Sessions * Hours * Rate;
        public string documentName => Path.GetFileName(DocumentPath);
        // Add this property to your Claim class
        public string Status { get; set; }
    }
}
