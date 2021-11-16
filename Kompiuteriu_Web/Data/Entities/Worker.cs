using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Entities
{
    public class Worker : IUserOwnedResource
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public double salary { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Required]
        public string User_id_saved { get; set; }
        public RestUser User { get; set; }
    }
}
