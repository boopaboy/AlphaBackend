using Auth.Managers;
using Auth.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;


namespace Auth.Services
{
    public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IdentityResult> SignUpAsync(SignupModel model)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == model.Email))
                return new IdentityResult();
            var result = await RegisterAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(model.Email);
                if (appUser != null)
                {
                    var updateProfileResult = await SetUserProfileAsync(appUser);
                }



            }


        }


        public async Task<IdentityResult> SetUserProfilea
        
        
        
        
        
        public async Task <IdentityResult> RegisterAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return new IdentityResult();
            var user = new AppUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user);
            return result;
                    

            
        }



    
    }
}
