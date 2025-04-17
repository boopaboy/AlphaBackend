using Microsoft.AspNet.Identity.EntityFramework;

namespace Auth.Models
{
    public class AppUser : IdentityUser
    {
        public AppUserProfile? Profile { get; set; }

        public AppUserAddress? Adress { get; set; }
    }
}
