using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Dtos.Computers
{
    public record UpdateComputerDto(string Computer_name, string Processor, string GPU, string RAM, string Storage, string PSU, int amount);
}
