using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class AppUserProfile
    {

        public string UserId { get; set; } = null!;

        [ProtectedPersonalData]
        public string? FirstName { get; set; }

        [ProtectedPersonalData]
        public string? LastName { get; set; }

        [ProtectedPersonalData]
        public string? JobTitle { get; set; }

        [ProtectedPersonalData]
        public string? PhoneNumber { get; set; }

        public AppUser User { get; set; } = null!;
    }
}
