using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Entities
{
    public class Shop : IUserOwnedResource
    {
        public int id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required]
        public string User_id_saved { get; set; }
        public RestUser User { get; set; }
    }
}
