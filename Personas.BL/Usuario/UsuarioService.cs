﻿using System;
using Personas.BE.Response;
using Personas.BE.Usuario;
using Personas.DAC.Persona;
using Personas.DAC.Usuario;
using Serilog;
using static Personas.BL.Helper.ResponseTransform;

namespace Personas.BL.Usuario
{
	public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepostiory;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepostiory = usuarioRepository;
        }

        public Response<List<Personas.BE.Usuario.UsuarioModel>> GetUsers()
        {
            Response<List<Personas.BE.Usuario.UsuarioModel>> response = new Response<List<Personas.BE.Usuario.UsuarioModel>>();
            Log.Information("Iniciando ejecucion GetUsers");
            try
            {
                List<Personas.BE.Usuario.UsuarioModel> users = _usuarioRepostiory.GetUsers();
                if (users == null)
                {
                    return CreateErrorResponseList(response, "No se encontraron datos de personas.");
                }
                return CreateSuccessResponseList(response, users);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "GetUsers");
                Log.Error(message);
                return CreateErrorResponseList(response, $"Revisar Log");
            }
        }

        public Response<Personas.BE.Usuario.UsuarioModel> GetUserForId(int userId)
        {
            Response<Personas.BE.Usuario.UsuarioModel> response = new Response<Personas.BE.Usuario.UsuarioModel>();
            Log.Information("Iniciando ejecucion GetUserForId");
            try
            {
                Personas.BE.Usuario.UsuarioModel user = _usuarioRepostiory.GetUser(userId);
                if (user == null)
                {
                    return CreateErrorResponse(response, "No se encontraron datos de usuarios.");
                }
                return CreateSuccessResponse(response, user);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "GetUserForId");
                Log.Error(message);
                return CreateErrorResponse(response, $"Revisar Log");
            }
        }

        public Response<Personas.BE.Usuario.UsuarioModel> UpdateUser(UpdateUserRequest updateUser)
        {
            Response<Personas.BE.Usuario.UsuarioModel> response = new Response<Personas.BE.Usuario.UsuarioModel>();
            Log.Information("Iniciando ejecucion UpdateUser");
            try
            {
                Personas.BE.Usuario.UsuarioModel user = _usuarioRepostiory.UpdateUser(updateUser);
                if (user == null)
                {
                    return CreateErrorResponse(response, "No se encontraron datos de usuarios.");
                }
                return CreateSuccessResponse(response, user);
            }
            catch (Exception ex)
            {
                string message = string.Format("Message: {0} AND MethodName = {1}", ex.Message, "UpdateUser");
                Log.Error(message);
                return CreateErrorResponse(response, $"Revisar Log");
            }
        }
    }
}

