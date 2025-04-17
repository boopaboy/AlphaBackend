using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class AppUserAddress
    {
        [PersonalData]
        public string UserId { get; set; } = null!;

        [PersonalData]
        public string?  StreetName { get; set; }

        [PersonalData]
        public string? PostalCode { get; set; }

        [PersonalData]

        public string? City { get; set; }

        public AppUser User { get; set; } = null!;
    }
}
