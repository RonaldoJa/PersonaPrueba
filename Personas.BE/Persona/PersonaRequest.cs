using System;
namespace Personas.BE.Persona
{
	public class PersonaRequest
	{
		public int PersonadoId { get; set; }
		public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Email { get; set; }
        public string TipoIdentificacion { get; set; }


    }
}

