using HairMate.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection.Emit;

namespace HairMate.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Salon>()
                .HasMany(s => s.Services)
                .WithOne(s => s.Salon)
                .HasForeignKey(s => s.SalonId);

            modelBuilder.Entity<Salon>()
                .HasMany(s => s.Employees)
                .WithOne(e => e.Salon)
                .HasForeignKey(e => e.SalonId);

            modelBuilder.Entity<Salon>()
                .HasMany(s => s.Reviews)
                .WithOne(r => r.Salon)
                .HasForeignKey(r => r.SalonId);
        }
    }
}
