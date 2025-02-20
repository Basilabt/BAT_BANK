



using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessAccessLayer;
using Microsoft.IdentityModel.Tokens;



namespace BAT_BANK_API.Services
{
    public class clsTokenService
    {

        private const string SecretKey = "Hi_Im_Basel_AbuTaleb_This_is_a_training_project_for_my_training_at_fact(^_^)"; 
        private const int ExpirationMinutes = 60; 

        public static string GenerateJWT(clsOnlineAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, account.onlineAccountID.ToString()),
                new Claim(ClaimTypes.Name, account.username),
            }),
                Expires = DateTime.UtcNow.AddMinutes(ExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
