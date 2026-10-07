using System.Configuration;
using System.Text;
using MeuDiarioSENAC.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
 
public class TokenService
{
    private readonly IConfiguration _configuration;
 
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GerarToken(Usuario usuario)
    {
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwtSettings = _configuration.GetSection("jwtsettings");
        var chaveSecretaBytes = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);
        var credenciais = new Microsoft.IdentityModel.Tokens.SigningCredentials(
            new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(chaveSecretaBytes),
            Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature
        );
        var descriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credenciais,
            Expires = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["TokenExpirationInHours"]))
        };
          var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }
}