using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personas.BE.Persona;
using Personas.BL.Persona;

namespace Personas.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonaService _persona;

        public PersonController(IPersonaService persona)
        {
            _persona = persona;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPerson()
        {
            var response = _persona.GetPerson();
            if (response.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("insert")]
        public IActionResult InsertPersonUser([FromBody] PersonaUsuarioModel personaUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo no valido"); 
            }

            var response = _persona.InsertPersonUser(personaUsuario);
            if (response.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);
            
        }

        [HttpPost("update")]
        public IActionResult UpdatePerson([FromBody] PersonaRequest personaRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo no valido");
            }

            var response = _persona.UpdatePerson(personaRequest);
            if (response.Error)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
    }
}

