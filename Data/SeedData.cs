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
                    (
                        "Wizz",
                        100,
                        20
                    ),
                    new Plane
                    (
                        "Wizz",
                        200,
                        30
                    ),
                    new Plane
                    (
                        "Rayan",
                        100,
                        20
                    ),
                    new Plane
                    (
                        "Wizz",
                        180,
                        0
                    ),
                    new Plane
                    (
                        "Delta",
                        16,
                        108
                    ));
                context.SaveChanges();
            }
        }
    }
}
