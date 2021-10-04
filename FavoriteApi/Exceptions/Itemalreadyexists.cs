using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteApi.Exceptions
{
    public class Itemalreadyexists : ApplicationException
    {
    public Itemalreadyexists(string message) : base(message) { }
    }
}
