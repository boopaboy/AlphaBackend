using Business.Services;
using Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public interface IJwtHandler
{
    Task<string> GenerateJwtToken(UserEntity user, IConfiguration configuration);
}

public class JwtHandler: IJwtHandler
{

  

    private readonly IUserService _userService;

    public JwtHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> GenerateJwtToken(UserEntity user, IConfiguration configuration)
    {
        var claimsList = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var roles = await _userService.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claimsList.Add(new Claim("role", role));
        }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims: claimsList,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}