using System;
using Personas.BE.Persona;

namespace Personas.DAC.Persona
{
	public interface IPersonaRepository
	{
        List<Personas.BE.Persona.Persona> GetPersons();
        bool InsertPersonAndUser(PersonaUsuarioModel personaUsuario);
        Personas.BE.Persona.Persona UpdatePerson(PersonaRequest personaRequest);

    }
}

