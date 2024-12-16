using System;
using Personas.BE.Response;

namespace Personas.BL.Helper
{
	public class ResponseTransform
	{
        public static Response<T> CreateSuccessResponse<T>(Response<T> response, T data)
        {
            response.Data = data;
            response.Message = "Operación realizada exitosamente";
            return response;
        }

        public static Response<T> CreateErrorResponse<T>(Response<T> response, string errorMessage)
        {
            response.Data = default;
            response.Message = errorMessage;
            response.Error = true;
            return response;
        }

        public static Response<List<T>> CreateSuccessResponseList<T>(Response<List<T>> response, List<T> data)
        {
            response.Data = data;
            response.Message = "Operación realizada exitosamente";
            return response;
        }

        public static Response<List<T>> CreateErrorResponseList<T>(Response<List<T>> response, string errorMessage)
        {
            response.Data = default;
            response.Message = errorMessage;
            response.Error = true;
            return response;
        }

    }
}

