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

        public virtual ICollection<Reservation>? Reservations { get; set; }

        public Flight()
        {

        }
        public Flight(Flight flight)
        {
            this.FlightID = flight.FlightID;
            this.StartDateTime = flight.StartDateTime;
            this.StartLocation = flight.StartLocation;
            this.EndLocation = flight.EndLocation;
            this.EndDateTime = flight.EndDateTime;
            this.PilotName = flight.PilotName;
            this.PlaneID = flight.PlaneID;
            this.Plane = new Plane(flight.Plane);
            this.Reservations = new List<Reservation>(flight.Reservations);
        }
    }
}
