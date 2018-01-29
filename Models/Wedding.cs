using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Weddings
    {
        [Key]
        public int weddingId { get; set; }
        public string bride { get; set; }
        public string groom { get; set; }
        public string address { get; set; }
        public DateTime date { get; set; }
        public int userId { get; set; }
        public User Planner { get; set; }
        public List<RSVP> guests {get;set;}
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

 
        public Weddings()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}