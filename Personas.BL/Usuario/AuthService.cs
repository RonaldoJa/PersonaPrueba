using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Personas.BE.Persona;
using Personas.BE.Response;
using Personas.BE.Usuario;
using Personas.BL.Helper;
using Personas.DAC.Usuario;
using Serilog;
using static Personas.BL.Helper.ResponseTransform;

namespace Personas.BL.Usuario
{
	public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepostiory;
        private readonly Encrypt _encrypt;


        public AuthService(IConfiguration configuration, IUsuarioRepository usuarioRepository, Encrypt encrypt)
        {
            _configuration = configuration;
            _usuarioRepostiory = usuarioRepository;
            _encrypt = encrypt;
        }

        public Response<LoginToken> LoginUser(LoginModel loginModel)
        {
            Response<LoginToken> response = new Response<LoginToken>();
            LoginResponse login = new LoginResponse();
            LoginToken loginToken = new LoginToken();
            Log.Information("Iniciando ejecucion LoginUser");
            try
            {

                login = _usuarioRepostiory.LoginUser(loginModel);
                if(login == null)
                    return CreateErrorResponse(response, "Usuario o Password Incorrecto");
                string decryptedPassword = _encrypt.DecryptPassword(login.Password);

                if(loginModel.Password == decryptedPassword)
                {
                    loginToken.Usuario = loginModel.Usuario;
                    loginToken.Token = GenerateJwtToken(loginModel.Usuario);

                    return CreateSuccessResponse(response, loginToken);
                }
                return CreateErrorResponse(response, "Usuario o Password Incorrecto");

            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "LoginUser");
                Log.Error(message);
                return CreateErrorResponse(response, $"Revisar Log");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]); 
            var tokenValidity = int.Parse(_configuration["Jwt:TokenValidityInMinutes"]); 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidity), 
                Issuer = _configuration["Jwt:Issuer"], 
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

