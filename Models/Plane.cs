using System.ComponentModel.DataAnnotations;
namespace proekt1.Models
{
    public class Plane
    {
        [Key]
        public int PlaneID { get; set; }

        public string Company { get; set; }
        public int MaxSeats { get; set; }
        public int MaxBusinessSeats { get; set; }

        public virtual ICollection<Flight>? Flights { get; set; }

        public Plane()
            { }

        public Plane(string company, int maxSeats, int maxBS)
        {
            this.Company = company;
            this.MaxSeats = maxSeats;
            this.MaxBusinessSeats = maxBS;
        }
        public Plane(Plane plane)
        {
            this.PlaneID = plane.PlaneID;
            this.Company = plane.Company;
            this.MaxSeats = plane.MaxSeats;
            this.MaxBusinessSeats = plane.MaxBusinessSeats;
            this.Flights = new List<Flight>(plane.Flights);
        }
    }
}
