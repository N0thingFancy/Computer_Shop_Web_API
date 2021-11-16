using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Dtos.Workers
{
    public record UpdateWorkerDto(string name, string lastName, string address, string phoneNumber, string email, double salary);
}
