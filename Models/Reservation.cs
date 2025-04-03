using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proekt1.Models
{
    public class Reservation
    {
        [Key]
        public string ReservationID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "It has to be 10 characters.")]
        public string EGN { get; set; }

        [Required]
        public string Phone { get; set; }

        [ScaffoldColumn(false)]
        public int PlaneID { get; set; }

        [Required]
        public string TicketType { get; set; }
        public int FlightID { get; set; }
        [ForeignKey("FlightID")]
        public Flight? Flight { get; set; }

        [ScaffoldColumn(false)]
        public string? UserEmail { get; set; }
        [ForeignKey("Email")]
        public Person? User { get; set; }
    }
}
