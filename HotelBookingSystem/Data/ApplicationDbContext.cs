using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace HotelBookingSystem.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=Hotel_App;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");
                entity.HasKey(r => r.RoomId);
                entity.Property(r => r.RoomType)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(r => r.MaxExtraBeds)
                      .IsRequired();
                entity.Property(r => r.IsAvailable)
                      .IsRequired();

                
                entity.HasMany(r => r.Bookings)
                      .WithOne(b => b.Room)
                      .HasForeignKey(b => b.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(c => c.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(c => c.PhoneNumber)
                      .HasMaxLength(15);
                entity.Property(c => c.Email)
                      .HasMaxLength(100);

             
                entity.HasMany(c => c.Bookings)
                      .WithOne(b => b.Customer)
                      .HasForeignKey(b => b.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Bookings");
                entity.HasKey(b => b.BookingId);

               
                entity.Property(b => b.CheckInDate)
                      .IsRequired()
                      .HasColumnType("date"); 

               
                entity.Property(b => b.CheckOutDate)
                      .IsRequired()
                      .HasColumnType("date"); 

                // Relationer
                entity.HasOne(b => b.Room)
                      .WithMany(r => r.Bookings)
                      .HasForeignKey(b => b.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Customer)
                      .WithMany(c => c.Bookings)
                      .HasForeignKey(b => b.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}

