using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proekt1.Models
{
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }

        [Required]
        public string StartLocation { get; set; }

        [Required]
        public string EndLocation { get; set; }

        [Required]
        [DateGreaterThanToday]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DateGreaterThanToday]
        public DateTime EndDateTime { get; set; }

        [Required]
        public string PilotName { get; set; }

        public int PlaneID { get; set; }
        [ForeignKey("PlaneID")]
        public Plane? Plane { get; set; }
        public virtual ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();


        
      
    }
}
