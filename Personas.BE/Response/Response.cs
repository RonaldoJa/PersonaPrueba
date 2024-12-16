using System;
namespace Personas.BE.Response
{
	public class Response<T>
	{
		public string Message { get; set; }
		public T Data { get; set; }
		public bool Error = false;
    }
}

