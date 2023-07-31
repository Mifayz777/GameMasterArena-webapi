using GameMasterArena.Domain.Entities.Persons;
using GameMasterArena.Service.Common.Helpers;
using GameMasterArena.Service.Interfaces.Persons;
using GetTalim.Service.Interfaces.Persons;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameMasterArena.Service.Services.Students;

public class TokenPersonService : ITokenPersonService
{
    private readonly IConfiguration _config;
    public TokenPersonService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }

    public string GenerateToken(Person person)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", person.Id.ToString()),
            new Claim("FirstName", person.FirstName),
            new Claim("LastName", person.LastName),
            new Claim(ClaimTypes.Email, person.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    

}
