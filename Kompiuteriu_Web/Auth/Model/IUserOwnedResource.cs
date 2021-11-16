using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Auth.Model
{
    public interface IUserOwnedResource
    {
        string User_id_saved { get; }
    }
}
