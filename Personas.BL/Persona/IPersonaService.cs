using System;
using Personas.BE.Persona;
using Personas.BE.Response;

namespace Personas.BL.Persona
{
	public interface IPersonaService
	{
        Response<List<Personas.BE.Persona.Persona>> GetPerson();
        Response<Personas.BE.Persona.Persona> InsertPersonUser(PersonaUsuarioModel personaUsuario);
    }
}

