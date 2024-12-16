using System;
using Personas.BE.Persona;
using System.Text.RegularExpressions;

namespace Personas.BL.Helper
{
    public class ValidationModels
    {
        public static List<string> ValidatePersonUser(PersonaUsuarioModel personaUsuario)
        {
            var errors = new List<string>();

            if (personaUsuario.NumeroIdentificacion.Length != 10 || !Regex.IsMatch(personaUsuario.NumeroIdentificacion, @"^\d{10}$"))
            {
                errors.Add("El número de identificación debe tener exactamente 10 dígitos.");
            }

            if (!Regex.IsMatch(personaUsuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {

                errors.Add("El formato del email no es válido.");
            }
            if (personaUsuario.Pass.Length < 8 ||
                !Regex.IsMatch(personaUsuario.Pass, @"\d") ||
                !Regex.IsMatch(personaUsuario.Pass, @"[!@#]"))
            {
                errors.Add("La contraseña debe tener al menos 8 caracteres, contener un número y uno de los siguientes signos: !, @, #.");
            }

            return errors;
        }
    }
}

