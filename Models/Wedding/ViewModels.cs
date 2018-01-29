using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Dashboard
    {
        public List<Weddings> Weddings { get; set; }
        public User User { get; set; }
    }

    public class ViewWedding
    {
        [Required]
        [Display(Name = "Bride's Name")]
        public string bride {get;set;}

        [Required]
        [Display(Name = "Groom's Name")]
        public string groom {get;set;}

        [Required]
        [FutureDate]
        [DataType(DataType.Date)]
        [Display(Name = "Wedding Date")]
        public DateTime date {get;set;}

        [Required]
        [Display(Name = "Wedding Location")]
        public string address {get;set;}
    }

    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = (DateTime)value;
            return date < DateTime.Now ? new ValidationResult("Date must be in future.") : ValidationResult.Success;
        }
    }
}