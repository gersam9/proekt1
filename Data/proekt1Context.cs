using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        public DbSet<proekt1.Models.Plane> Plane { get; set; } = default!;

        //promqna
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.FlightID);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Plane)
                .WithMany(p => p.Flights)
                .HasForeignKey(f => f.PlaneID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserEmail);
        }
    }
}
