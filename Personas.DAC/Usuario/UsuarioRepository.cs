using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Personas.DAC.Helper;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using Personas.BE.Usuario;

namespace Personas.DAC.Usuario
{
	public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConnectionDataAccess _dbaccess;
        public UsuarioRepository(IConfiguration configuration)
        {
            _dbaccess = new ConnectionDataAccess(configuration, "Persona");
        }

        public LoginResponse LoginUser(LoginModel loginModel)
        {
            DataSet dataSet;
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter { ParameterName = "@Usuario", SqlDbType = SqlDbType.VarChar, Size = 50, Value = loginModel.Usuario });
            dataSet = _dbaccess.ExecuteFillSp("sp_LoginUsuario", parameters.ToArray());

            if (dataSet.Tables[0].Rows.Count == 0)
                return null;

            DataRow row = dataSet.Tables[0].Rows[0];

            LoginResponse login = new LoginResponse();
            login.Usuario = row.GetCell<string>("Usuario");
            login.Password = row.GetCell<string>("Pass");
            login.PersonaId = row.GetCell<int>("PersonaId");

            return login;

        }

        public List<Personas.BE.Usuario.UsuarioModel> GetUsers()
        {

            DataSet dataSet;
            dataSet = _dbaccess.ExecuteFillSp("sp_ConsultarUsuarios", null);

            if (dataSet.Tables[0].Rows.Count == 0)
                return null;

            List<Personas.BE.Usuario.UsuarioModel> usuarios = new List<Personas.BE.Usuario.UsuarioModel>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                Personas.BE.Usuario.UsuarioModel usuario = new Personas.BE.Usuario.UsuarioModel
                {
                    UsuarioId = dataRow.GetCell<int>("UsuarioId"),
                    Usuario = dataRow.GetCell<string>("Usuario"),
                    FechaCreacionUsuario = dataRow.GetCell<DateTime>("FechaCreacionUsuario"),
                    PersonaId = dataRow.GetCell<int>("PersonaId"),
                    Nombres = dataRow.GetCell<string>("Nombres"),
                    Apellidos = dataRow.GetCell<string>("Apellidos"),
                    NumeroIdentificacion = dataRow.GetCell<string>("NumeroIdentificacion"),
                    Email = dataRow.GetCell<string>("Email"),
                    TipoIdentificacion = dataRow.GetCell<string>("TipoIdentificacion"),
                    FechaCreacionPersona = dataRow.GetCell<DateTime>("FechaCreacionPersona"),
                    NumeroIdentificacionCompleto = dataRow.GetCell<string>("NumeroIdentificacionCompleto"),
                    NombreCompleto = dataRow.GetCell<string>("NombreCompleto")
                };

                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}

