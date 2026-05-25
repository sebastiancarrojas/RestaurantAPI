using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Domain.Entities;

namespace RestaurantAPI.DataAccess;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // Customer 1:N Reservation
        // =========================
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CustomerId);

        // =========================
        // Table 1:N Reservation
        // =========================
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Table)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.TableId);

        // =========================
        // Reservation 1:1 Order
        // =========================
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Order)
            .WithOne(o => o.Reservation)
            .HasForeignKey<Order>(o => o.ReservationId);

        //Restaurant
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Address).HasMaxLength(200);
            entity.Property(r => r.Phone).HasMaxLength(20);
            entity.Property(r => r.Email).HasMaxLength(100);
            entity.HasIndex(r => r.Name).IsUnique();
        });
    }
}