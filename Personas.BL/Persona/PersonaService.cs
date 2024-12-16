using System;
using Microsoft.Data.SqlClient;
using Personas.BE.Persona;
using Personas.BE.Response;
using Personas.BL.Helper;
using Personas.DAC.Persona;
using Serilog;
using static Personas.BL.Helper.ResponseTransform;
using static Personas.BL.Helper.ValidationModels;

namespace Personas.BL.Persona
{
	public class PersonaService : IPersonaService
    {
		private readonly IPersonaRepository _persona;
        private readonly Encrypt _encrypt;
		public PersonaService(IPersonaRepository persona, Encrypt encrypt)
		{
			_persona = persona;
            _encrypt = encrypt;
		}

        public Response<List<Personas.BE.Persona.Persona>> GetPerson()
        {
            Response<List<Personas.BE.Persona.Persona>> response = new Response<List<Personas.BE.Persona.Persona>>();
            Log.Information("Iniciando ejecucion GetPerson");
            try
            {
                List<Personas.BE.Persona.Persona> persons = _persona.GetPersons(); 
                if (persons == null)
                {
                    return CreateErrorResponseList(response, "No se encontraron datos de personas.");
                }
                return CreateSuccessResponseList(response, persons);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "GetPerson");
                Log.Error(message);
                return CreateErrorResponseList(response, $"Revisar Log");
            }
        }

        public Response<Personas.BE.Persona.Persona> UpdatePerson(PersonaRequest personaRequest)
        {
            Response<Personas.BE.Persona.Persona> response = new Response<Personas.BE.Persona.Persona>();
            Log.Information("Iniciando ejecucion UpdatePerson");
            try
            {
                Personas.BE.Persona.Persona person = _persona.UpdatePerson(personaRequest);
                if (person == null)
                {
                    return CreateErrorResponse(response, "No se encontraron datos de personas.");
                }
                return CreateSuccessResponse(response, person);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "UpdatePerson");
                Log.Error(message);
                return CreateErrorResponse(response, $"Revisar Log");
            }
        }

        public Response<Personas.BE.Persona.Persona> InsertPersonUser(PersonaUsuarioModel personaUsuario)
        {
            Response<Personas.BE.Persona.Persona> response = new Response<Personas.BE.Persona.Persona>();
            Log.Information("Iniciando ejecucion InsertPersonUser");
            try
            {
                List<string> validationErrors = ValidatePersonUser(personaUsuario);
                if (validationErrors.Any())
                {
                    return CreateErrorResponse(response, string.Join(", ", validationErrors));
                }

                personaUsuario.Pass = _encrypt.EncryptPassword(personaUsuario.Pass);

                bool isValidInsert = _persona.InsertPersonAndUser(personaUsuario);
                if (!isValidInsert)
                {
                    return CreateErrorResponse(response, "Usuario, email o identificion existentes");
                }
                return CreateSuccessResponse(response, default);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "InsertPersonUser");
                Log.Error(message);
                return CreateErrorResponse(response, $"Revisar Log");
            }
        }

    }
}

