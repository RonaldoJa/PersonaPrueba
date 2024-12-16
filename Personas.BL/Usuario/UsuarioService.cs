using System;
using Personas.BE.Response;
using Personas.DAC.Persona;
using Personas.DAC.Usuario;
using Serilog;
using static Personas.BL.Helper.ResponseTransform;

namespace Personas.BL.Usuario
{
	public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepostiory;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepostiory = usuarioRepository;
        }

        public Response<List<Personas.BE.Usuario.UsuarioModel>> GetUsers()
        {
            Response<List<Personas.BE.Usuario.UsuarioModel>> response = new Response<List<Personas.BE.Usuario.UsuarioModel>>();
            Log.Information("Iniciando ejecucion GetUsers");
            try
            {
                List<Personas.BE.Usuario.UsuarioModel> users = _usuarioRepostiory.GetUsers();
                if (users == null)
                {
                    return CreateErrorResponseList(response, "No se encontraron datos de personas.");
                }
                return CreateSuccessResponseList(response, users);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "GetUsers");
                Log.Error(message);
                return CreateErrorResponseList(response, $"Revisar Log");
            }
        }
    }
}

