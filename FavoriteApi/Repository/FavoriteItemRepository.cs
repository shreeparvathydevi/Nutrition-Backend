using FavoriteApi.Models;
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
            List<FavoriteItem> favourites = await _db.Favorites.Where(x => x.userEmail == userEmail).ToListAsync();
            if (favourites.Count != 0)
            {
                return favourites;
            }
            else
                return null;
        }
        public async Task<bool> AddFavourites(string userEmail, Favourite favourite)
        {
            UserFavourite user = await _db.UserFavourites.FirstOrDefaultAsync(x => x.userEmail == userEmail);
            if (user == null)
            {
                UserFavourite userFavourite = new UserFavourite() { userEmail = userEmail, favourites = new List<Favourite>() { favourite } };
                _db.UserFavourites.Add(userFavourite);
            }
            else
            {
                favourite.userEmail = userEmail;
                _db.Favourites.Add(favourite);
            }
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveFavourites(string userEmail, string favouriteId)
        {
            List<Favourite> favourites = await _db.Favourites.Where(x => x.userEmail == userEmail).ToListAsync();
            if (favourites.Count != 0)
            {
                Favourite fav = favourites.FirstOrDefault(x => x.id == favouriteId);
                if (fav != null)
                {

                    foreach (Favourite x in _db.Favourites)
                    {
                        if (x.id == favouriteId)
                        {
                            _db.Favourites.Remove(x);
                            break;
                        }
                    }
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<bool> IsNewsExist(string userEmail, string favouriteId)
        {
            List<Favourite> favourites = await _db.Favourites.Where(x => x.userEmail == userEmail).ToListAsync();
            if (favourites.Count != 0)
            {
                Favourite fav = favourites.FirstOrDefault(x => x.id == favouriteId);
                if (fav != null)
                    return true;

            }
            return false;
        }

    }
    
}
