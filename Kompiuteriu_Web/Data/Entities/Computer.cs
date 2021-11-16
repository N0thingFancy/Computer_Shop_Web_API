using Kompiuteriu_Web.Auth.Model;
using Kompiuteriu_Web.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Entities
{
    public class Computer : IUserOwnedResource
    {
        public int id { get; set; }
        public string Computer_name { get; set; }
        public string Processor { get; set; }
        public string GPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string PSU { get; set; }
        public int amount { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Required]
        public string User_id_saved { get; set; }
        public RestUser User { get; set; }
    }
}
