using DotNetEnv;

namespace TestCons;

using Microsoft.EntityFrameworkCore;

public class TaxiDbContext : DbContext
{
    public DbSet<TaxiTrip> TaxiTrips { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        Env.Load(Program.path);

        string connString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connString))
        {
            throw new Exception("no env");
        }

        optionsBuilder.UseNpgsql(connString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxiTrip>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<TaxiTrip>()
            .HasIndex(t => t.PULocationID);

        modelBuilder.Entity<TaxiTrip>()
            .HasIndex(t => t.trip_distance);

        modelBuilder.Entity<TaxiTrip>()
            .HasIndex(t => new { t.tpep_pickup_datetime, t.tpep_dropoff_datetime });
    }
}
