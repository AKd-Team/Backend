using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Academic.Services;

namespace Academic.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }
        //invoca atasarea UserContextului
        public async Task Invoke(HttpContext context, IUsersService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserContext(context, userService, token);
            await _next(context);
        }
        private void attachUserContext(HttpContext context, IUsersService userService, string token)
        {
            try
            {
                //genereaza token
                var tokenHandler = new JwtSecurityTokenHandler();
                //genereaza secret
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                //valideaa tokenul de validare
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);//daca e bine, da tokenul pt requesturi

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);//genereaza userId-ul pe baza tokenului
                context.Items["Users"] = userService.GetById(userId);//returneaza userul pt tokenul generat
            }
            catch
            {
                //do nothing if it fails
            }
        }
    }
}