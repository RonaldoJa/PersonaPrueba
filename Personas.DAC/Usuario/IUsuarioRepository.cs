using System;
using Personas.BE.Usuario;

namespace Personas.DAC.Usuario
{
	public interface IUsuarioRepository
	{
        public List<Personas.BE.Usuario.UsuarioModel> GetUsers();
        LoginResponse LoginUser(LoginModel loginModel);
        Personas.BE.Usuario.UsuarioModel GetUser(int userId);

    }
}

