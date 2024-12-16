using System;
using Personas.BE.Response;

namespace Personas.BL.Usuario
{
	public interface IUsuarioService
	{
        Response<List<Personas.BE.Usuario.UsuarioModel>> GetUsers();
        Response<Personas.BE.Usuario.UsuarioModel> GetUserForId(int userId);

    }
}

