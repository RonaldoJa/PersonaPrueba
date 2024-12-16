using System;
namespace Personas.BE.Usuario
{
	public class UsuarioModel
	{
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }

        public DateTime FechaCreacionUsuario { get; set; }

        public int PersonaId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Email { get; set; }
        public string TipoIdentificacion { get; set; }
        public DateTime FechaCreacionPersona { get; set; }
        public string NumeroIdentificacionCompleto { get; set; }
        public string NombreCompleto { get; set; }
    }
}

