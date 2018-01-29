using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int rsvpId { get; set; }
        public int userId { get; set; }
        public User Guest { get; set; }
        public int weddingId { get; set; }
        public Weddings Wedding { get; set; } 
        // public int users_userId { get; set; }
       
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

 
        public RSVP()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}