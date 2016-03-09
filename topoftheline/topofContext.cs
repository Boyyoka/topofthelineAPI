namespace topoftheline
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class topofContext : DbContext
    {
        public topofContext()
            : base("name=topFoodContext")
        {

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasMany(e => e.Restaurants)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Food)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Restaurant>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Restaurant)
                .WillCascadeOnDelete(false);
        }
    }
}
