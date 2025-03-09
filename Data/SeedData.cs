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
                context.SaveChanges();
            }
        }
    }
}
