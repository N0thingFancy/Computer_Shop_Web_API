using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Dtos.Shops
{
    public record CreateShopDto(string City, string Address, string PhoneNumber, string Email);
}
