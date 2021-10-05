using FavoriteApi.Exceptions;
using FavoriteApi.Models;
using FavoriteApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FavoriteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteItemController : ControllerBase

    {
        FavoriteItemService _services;
        string userEmail = string.Empty;
        public FavoriteItemController(FavoriteItemService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userIdkey = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "emailId");
                if (userIdkey != null)
                {
                    userEmail = userIdkey.Value.ToString();
                    List<FavoriteItem> favourites = await _services.GetFavourites(userEmail);
                    return Ok(favourites);
                }
                throw new System.UnauthorizedAccessException("Invalid User");
            }
            catch (ItemNotFound e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FavoriteItem favourite)
        {
            try
            {
                var userIdkey = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "emailId");
                if (userIdkey != null)
                {
                   
                    userEmail = userIdkey.Value.ToString();
                    favourite.userEmail = userEmail;
                    bool flag = await _services.AddFavourites( favourite);
 
                    return Created("", flag);
                }
                throw new UnauthorizedAccessException("Invalid User");
            }
            catch (Itemalreadyexists e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        //[HttpDelete]
        //[Route("{trackId}")]
        //public async Task<IActionResult> Remove(string trackId)
        //{
        //    try
        //    {
        //        var userIdkey = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "emailId");
        //        if (userIdkey != null)
        //        {
        //            userEmail = userIdkey.Value.ToString();
        //            bool flag = await _services.RemoveFavourites(userEmail, trackId);
        //            return Ok(flag);
        //        }
        //        throw new UnauthorizedAccessException("Invalid User");
        //    }
        //    catch (ItemNotFound e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
       // }
    }
}
