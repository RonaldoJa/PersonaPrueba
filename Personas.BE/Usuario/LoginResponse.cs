using System;
namespace Personas.BE.Usuario
{
	public class LoginResponse
	{
		public string Usuario { get; set; }
		public string Password { get; set; }
		public int PersonaId { get; set; }
	}
}

