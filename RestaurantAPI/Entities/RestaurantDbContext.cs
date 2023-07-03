using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext: DbContext
    {
        private string _connectionString = "Data Source=DESKTOP-MRCK2V7;Initial Catalog=RestaurantDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";   
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                        .Property(r => r.Name)
                        .IsRequired()
                        .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                        .Property(d => d.Name)
                        .IsRequired();

            modelBuilder.Entity<Address>()
                        .Property(r => r.City)
                        .IsRequired()
                        .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                        .Property(r => r.Region)
                        .IsRequired()
                        .HasMaxLength(50);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
