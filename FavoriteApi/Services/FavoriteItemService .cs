using FavoriteApi.Exceptions;
using FavoriteApi.Models;
using FavoriteApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoriteApi.Services
{
    public class FavoriteItemService 
    {
        FavoriteItemRepository _repository;
        public FavoriteItemService(FavoriteItemRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<FavoriteItem>> GetFavourites(string userEmail)
        {
            List<FavoriteItem> favourites = await _repository.GetFavourites(userEmail);
            if (favourites != null)
                return favourites;
            else
                throw new ItemNotFound("No Favourites Available for This User");
        }
        public async Task<bool> AddFavourites(string userEmail, FavoriteItem favourite)
        {
            bool flag = await _repository.IsNewsExist(userEmail, favourite.id);
            if (!flag)
                return await _repository.AddFavorites(userEmail, favourite);
            else
                throw new Itemalreadyexists("This Track Already Exist in Favourites");
        }
        public async Task<bool> RemoveFavourites(string userEmail, string favouriteId)
        {
            bool flag = await _repository.IsNewsExist(userEmail, favouriteId);
            if (flag)
                return await _repository.RemoveFavourites(userEmail, favouriteId);
            else
                throw new TrackNotFoundException("No Track Available to Remove");
        }
    }
}
