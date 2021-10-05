using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Models
{

    public class FavoriteItemDbContext : DbContext
    {
        public FavoriteItemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<foodNutriants> foodNutriants { get; set; }
        public DbSet<FavoriteItem> FavouriteItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<foodNutriants>();
            user.HasKey(x => x.nutrientId);
            user.HasOne(x => x.favoriteItem).WithMany(x => x.foodNutrients).HasForeignKey(x => x.fdcId);
            var favourite = builder.Entity<FavoriteItem>();
            favourite.HasKey(x => x.fdcId);
           // favourite.HasMany(x => x.foodNutriants).WithOne(x => x.favourites).HasForeignKey(x => x.userEmail).IsRequired();
        }
    }
}
