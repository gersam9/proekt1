﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace proekt1.Models
{
    public class Person:IdentityUser
    {
        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EGN { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
