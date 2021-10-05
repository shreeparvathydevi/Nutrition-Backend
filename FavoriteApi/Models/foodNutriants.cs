using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Models
{
    public class foodNutriants
    {
       
        public string nutrientId { get; set; }
        public string nutrientName { get; set; }
        
        public string unitName { get; set; }
        public string derivationCode { get; set; }
        public string value { get; set; }
        public int fdcId { get; set; }
        public FavoriteItem favoriteItem { get; set; }
    }
}
