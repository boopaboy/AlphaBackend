using Authentication.Dtos;
using Authentication.Services.Models;
using Business.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Authentication.Services
{

    public interface IAuthService
    {
        Task<SignUpResult> SignUpAsync(SignUpForm model);
        Task<LoginResult> SignInAsync(string email, string password);

    }


    public class AuthService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IJwtHandler jwtHandler) : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IJwtHandler _jwtHandler = jwtHandler;





        public async Task<SignUpResult> SignUpAsync(SignUpForm model)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == model.Email))
                return new SignUpResult { Succeeded = false, ErrorMessage = "User already exists" };

            var result = await RegisterAsync(model.Email, model.Password);
            if (result.Succeeded)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User != null)
                {
                    User.FirstName = model.FirstName;
                    User.LastName = model.LastName;
                    User.PhoneNumber = model.PhoneNumber;
                    User.StreetName = model.StreetName;
                    User.PostalCode = model.PostalCode;
                    User.City = model.City;
                    await _userManager.UpdateAsync(User);
                    return new SignUpResult { Succeeded = true };
                }

                return new SignUpResult { Succeeded = true, ErrorMessage = "Succeeded with errors" };

            }

            return new SignUpResult { Succeeded = false, ErrorMessage = "Unable to create user account." };

        }

        public async Task<LoginResult> SignInAsync(string email, string password)
        {
            var result = await AuthenticateAsync(email, password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var token = await _jwtHandler.GenerateJwtToken(user, _configuration);
                    return new LoginResult { Succeeded = true, Token = token };
                }
            }

            return new LoginResult { Succeeded = false, Error = "Unable to login" };
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))  
                return IdentityResult.Failed(new IdentityError { Description = "email and password can't be null" });

            var User = new UserEntity
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(User, password);
            if (result.Succeeded)
            {
               await _userManager.AddToRoleAsync(User, "User");
            }
            
            return result;
        }

        public async Task<SignInResult> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return SignInResult.Failed;

            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result;
        }


    }
}
