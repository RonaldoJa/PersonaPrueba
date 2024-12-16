using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personas.BE.Response;
using Personas.BE.Usuario;
using Personas.BL.Persona;
using Personas.BL.Usuario;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Personas.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuario;
        private readonly IAuthService _authService;

        public UsuarioController(IUsuarioService usuario, IAuthService authService)
        {
            _usuario = usuario;
            _authService = authService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            Response<List<UsuarioModel>> response = _usuario.GetUsers();
            if (response.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            try
            {
                Response<LoginToken> response = _authService.LoginUser(loginModel);
                if (response.Error)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                string error = ex.GetBaseException().Message;
                return StatusCode(500, new { message = $"Ocurrió un error inesperado: {error}" });
            }

        }

        [HttpGet("consultarUsuario/{usuarioId}")]
        public IActionResult ConsultarUsuario(int usuarioId)
        {
            Response<UsuarioModel> response = _usuario.GetUserForId(usuarioId);
            if (response.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}

