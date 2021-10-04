using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Models
{
    public class FavoriteItem
    {
        public string Id { get; set; }
        public int fdcId { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string dataType { get; set; }
        public string brandOwner { get; set; }
        public string brandName { get; set; }
        public string foodCategory { get; set; }
        public string ingredients { get; set; }
        public string foodNutrients { get; set; }
        

    }
}
