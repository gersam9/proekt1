using Microsoft.EntityFrameworkCore;
using proekt1.Models;


namespace proekt1.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new proekt1Context(
            serviceProvider.GetRequiredService<
                    DbContextOptions<proekt1Context>>()))
            {
                // Проверява за самолети
                if (context.Plane.Any())
                {
                    return;   // Списъкът със самолети вече е запълнен
                }
                context.Plane.AddRange(
                    new Plane
                    {
                        Company = "Wizz",
                        MaxSeats = 100,
                        MaxBusinessSeats = 20
                    },
                    new Plane
                    {
                        Company = "Wizz",
                        MaxSeats = 200,
                        MaxBusinessSeats = 30
                    },
                    new Plane
                    {
                        Company = "Rayan",
                        MaxSeats = 100,
                        MaxBusinessSeats = 20
                    },
                    new Plane
                    {
                        Company = "Wizz",
                        MaxSeats = 180,
                        MaxBusinessSeats = 0
                    },
                    new Plane
                    {
                        Company = "Delta",
                        MaxSeats = 16,
                        MaxBusinessSeats = 108
                    });

                if(context.Flight.First(x => x.StartLocation == "London" && x.EndLocation == "Sofia" && x.StartDateTime == new DateTime(2025, 6, 18, 10, 30, 0) &&
                        x.EndDateTime == new DateTime(2025, 6, 18, 14, 0, 0) && x.PilotName == "Asen" && x.PlaneID == 1) == null)
                {
                    return;
                }
                context.Flight.AddRange

                    (new Flight
                    {
                        StartLocation = "London",
                        EndLocation = "Sofia",
                        StartDateTime = new DateTime(2025, 6, 18, 10, 30, 0),
                        EndDateTime = new DateTime(2025, 6, 18, 14, 0, 0),
                        PilotName = "Asen",
                        PlaneID = 1
                    },
                    new Flight
                    {
                        StartLocation = "Paris",
                        EndLocation = "Varna",
                        StartDateTime = new DateTime(2025, 6, 18, 11, 30, 0),
                        EndDateTime = new DateTime(2025, 6, 18, 16, 30, 0),
                        PilotName = "Atanas",
                        PlaneID = 2
                    },
                    new Flight
                    {
                        StartLocation = "Moskwa",
                        EndLocation = "Sofia",
                        StartDateTime = new DateTime(2025, 6, 19, 20, 0, 0),
                        EndDateTime = new DateTime(2025, 6, 20, 1, 30, 0),
                        PilotName = "Kristian",
                        PlaneID = 3
                    },
                        new Flight
                        {
                            StartLocation = "Varna",
                            EndLocation = "Sofia",
                            StartDateTime = new DateTime(2025, 6, 18, 9, 10, 0),
                            EndDateTime = new DateTime(2025, 6, 18, 10, 0, 0),
                            PilotName = "Veselin",
                            PlaneID = 4
                        },
                        new Flight
                        {
                            StartLocation = "Viena",
                            EndLocation = "Sofia",
                            StartDateTime = new DateTime(2025, 6, 19, 9, 30, 0),
                            EndDateTime = new DateTime(2025, 6, 19, 14, 0, 0),
                            PilotName = "Teodor",
                            PlaneID = 2
                        });
                context.SaveChanges();
            }
        }
    }
}
