using System;
using Personas.BE.Response;
using Personas.BE.Usuario;

namespace Personas.BL.Usuario
{
	public interface IAuthService
	{
        Response<LoginToken> LoginUser(LoginModel loginModel);

    }
}

