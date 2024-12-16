using System;
using Personas.BE.Response;
using Personas.BE.Usuario;

namespace Personas.BL.Usuario
{
	public interface IUsuarioService
	{
        Response<List<Personas.BE.Usuario.UsuarioModel>> GetUsers();
        Response<Personas.BE.Usuario.UsuarioModel> GetUserForId(int userId);
        Response<Personas.BE.Usuario.UsuarioModel> UpdateUser(UpdateUserRequest updateUser);

    }
}

