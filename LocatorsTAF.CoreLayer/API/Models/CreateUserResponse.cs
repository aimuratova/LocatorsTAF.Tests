using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.CoreLayer.API.Models
{
    public class CreateUserResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
    }
}
