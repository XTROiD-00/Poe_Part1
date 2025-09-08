using System;

namespace Poe_Part1.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int ModuleId { get; set; }
        public int Sessions { get; set; }
        public int Hours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string SubmittedTime { get; set; }
        public DateTime? PreApprovedDate { get; set; }
        public string PreApprovedTime { get; set; }
        public DateTime? FinalizedDate { get; set; }
        public string Document { get; set; }
    }
}

