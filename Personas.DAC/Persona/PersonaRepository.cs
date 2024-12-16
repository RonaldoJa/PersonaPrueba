using System;
using Microsoft.Extensions.Configuration;
using Personas.DAC.Helper;
using Microsoft.Data.SqlClient;
using System.Data;
using Personas.BE;
using Personas.BE.Persona;

namespace Personas.DAC.Persona
{
	public class PersonaRepository : IPersonaRepository
    {
		private readonly ConnectionDataAccess _dbaccess;
        public PersonaRepository(IConfiguration configuration)
        {
            _dbaccess = new ConnectionDataAccess(configuration, "Persona");
        }

        public List<Personas.BE.Persona.Persona> GetPersons()
		{
            List<SqlParameter> parameters = new List<SqlParameter>();

            DataSet dataSet;
            dataSet = _dbaccess.ExecuteFillSp("sp_ConsultarPersonas", null);

            if (dataSet.Tables[0].Rows.Count == 0)
                return null;

            List<Personas.BE.Persona.Persona> personas = new List<Personas.BE.Persona.Persona>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                Personas.BE.Persona.Persona persona = new Personas.BE.Persona.Persona
                {
                    Identificador = dataRow.GetCell<int>("Identificador"),
                    Nombres = dataRow.GetCell<string>("Nombres"),
                    Apellidos = dataRow.GetCell<string>("Apellidos"),
                    NumeroIdentificacion = dataRow.GetCell<string>("NumeroIdentificacion"),
                    Email = dataRow.GetCell<string>("Email"),
                    TipoIdentificacion = dataRow.GetCell<string>("TipoIdentificacion"),
                    FechaCreacion = dataRow.GetCell<DateTime>("FechaCreacion"),
                    NumeroIdentificacionCompleto = dataRow.GetCell<string>("NumeroIdentificacionCompleto"),
                    NombreCompleto = dataRow.GetCell<string>("NombreCompleto")
                };

                personas.Add(persona);
            }

            return personas;
        }

        public bool InsertPersonAndUser(PersonaUsuarioModel personaUsuario)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@Nombres", SqlDbType = SqlDbType.VarChar, Size = 100, Value = personaUsuario.Nombres });
            parameters.Add(new SqlParameter { ParameterName = "@Apellidos", SqlDbType = SqlDbType.VarChar, Size = 100, Value = personaUsuario.Apellidos });
            parameters.Add(new SqlParameter { ParameterName = "@NumeroIdentificacion", SqlDbType = SqlDbType.VarChar, Size = 50, Value = personaUsuario.NumeroIdentificacion });
            parameters.Add(new SqlParameter { ParameterName = "@Email", SqlDbType = SqlDbType.VarChar, Size = 150, Value = personaUsuario.Email });
            parameters.Add(new SqlParameter { ParameterName = "@TipoIdentificacion", SqlDbType = SqlDbType.VarChar, Size = 20, Value = personaUsuario.TipoIdentificacion });
            parameters.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = SqlDbType.VarChar, Size = 20, Value = personaUsuario.Usuario });
            parameters.Add(new SqlParameter { ParameterName = "@Pass", SqlDbType = SqlDbType.VarChar, Size = 50, Value = personaUsuario.Pass });

            int rowsAffected = _dbaccess.ExecuteSPQuery("sp_InsertarPersonaYUsuario", parameters.ToArray());
            if (rowsAffected <= 0)
                return false;
            return true;
        }
	}
}

