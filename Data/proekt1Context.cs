using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using proekt1.Models;

namespace proekt1.Data
{
    public class proekt1Context : DbContext
    {
        public proekt1Context (DbContextOptions<proekt1Context> options)
            : base(options)
        {
        }

        public DbSet<proekt1.Models.Flight> Flight { get; set; } = default!;
        public DbSet<proekt1.Models.Reservation> Reservation { get; set; } = default!;
    }
}
