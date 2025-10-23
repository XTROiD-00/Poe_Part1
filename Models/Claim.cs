using System.ComponentModel.DataAnnotations;

namespace Poe_Part1.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }  // Auto-generated unique identifier
        public string FacultyName { get; set; }
        public string ModuleName { get; set; }
        public string Sessions { get; set; }
        public int Hours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public string DocumentPath { get; set; } // Path to uploaded file
    }
}

