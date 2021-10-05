using FavoriteApi.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Repository
{

    public class FavoriteItemRepository
    {
        FavoriteItemDbContext _db;
        public FavoriteItemRepository(FavoriteItemDbContext db)
        {
            _db = db;
        }
        public async Task<List<FavoriteItem>> GetFavourites(string userEmail)
        {
            List<FavoriteItem> favourites = await _db.FavouriteItems.Where(x => x.userEmail == userEmail).ToListAsync();
            if (favourites.Count != 0)
            {
               foreach(var item in favourites)
                { 
                    
                   var food = _db.foodNutriants.Where(x => x.fdcId == item.fdcId).ToList();
                    item.foodNutrients = food;
                }
                    return favourites;
            }
            else
                return null;
        }
        public async Task<bool> AddFavourites( FavoriteItem favourite)
        {
              FavoriteItem fav = await _db.FavouriteItems.FirstOrDefaultAsync(x => x.fdcId == favourite.fdcId);
            if (fav == null)
            {
                //FavoriteItem  fav1 = new FavoriteItem() { fav = userEmail, favourites = new List<Favourite>() { favourite } };
            
               _db.FavouriteItems.Add(favourite);

                await _db.SaveChangesAsync();
                return true;
            }


            else
            {
                return false;
            }
            
        }
       // public async Task<bool> RemoveFavourites(string userEmail, string favouriteId)
        //{
        //    List<Favourite> favourites = await _db.Favourites.Where(x => x.userEmail == userEmail).ToListAsync();
        //    if (favourites.Count != 0)
        //    {
        //        Favourite fav = favourites.FirstOrDefault(x => x.id == favouriteId);
        //        if (fav != null)
        //        {

        //            foreach (Favourite x in _db.Favourites)
        //            {
        //                if (x.id == favouriteId)
        //                {
        //                    _db.Favourites.Remove(x);
        //                    break;
        //                }
        //            }
        //            await _db.SaveChangesAsync();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //        return false;
        //}
        //public async Task<bool> IsNewsExist(string userEmail, string favouriteId)
        //{
        //    List<Favourite> favourites = await _db.Favourites.Where(x => x.userEmail == userEmail).ToListAsync();
        //    if (favourites.Count != 0)
        //    {
        //        Favourite fav = favourites.FirstOrDefault(x => x.id == favouriteId);
        //        if (fav != null)
        //            return true;

        //    }
        //    return false;
        //

    }
    
}
