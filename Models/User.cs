using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public List<RSVP> rsvps {get;set;}
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

 
        public User()
        {
            created_at = DateTime.Now;
            updated_at = DateTime.Now;
        }
    }
}