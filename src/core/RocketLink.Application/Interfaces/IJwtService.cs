using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RocketLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RocketLink.Application.Interfaces;

public interface IJwtService
{
    string GetToken(User user);
}

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration Configuration = configuration;

    public string GetToken(User user)
    {
        var jwtOptions = new JwtOptions();
        Configuration.GetSection(JwtOptions.Position).Bind(jwtOptions);

        var claims = new List<Claim>
        {
            new Claim("email", user.Email),
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class JwtOptions
{
    public const string Position = "JWT";
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
