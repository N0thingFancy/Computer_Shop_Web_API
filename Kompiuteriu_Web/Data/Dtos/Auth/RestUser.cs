﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Dtos.Auth
{
    public class RestUser : IdentityUser
    {
        [PersonalData]
        public string AdditionalInfo { get; set; }
    }
}
