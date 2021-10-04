using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Models
{
    
    public class FavoriteItemDbContext: DbContext
    {
        public FavoriteItemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserFavorite> UserFavourites { get; set; }
        public DbSet<FavoriteItem> Favourites { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<UserFavourite>();
            user.HasKey(x => x.userEmail);
            var favourite = builder.Entity<FavoriteItem>();
            favourite.HasKey(x => x.fdcId);
            favourite.HasOne(x => x.UserFavourite).WithMany(x => x.favourites).HasForeignKey(x => x.userEmail).IsRequired();
        }
    }
}
