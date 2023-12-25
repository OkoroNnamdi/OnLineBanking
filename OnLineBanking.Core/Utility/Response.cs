using OnLineBanking.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
    public  class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Response(int statusCode, bool succeeded, string message, T data)
        {
            Data = data;
            Succeeded = succeeded;
            Message = message;
            StatusCode = statusCode;
        }
        public static Response<T> Fail(string errorMessage, int statusCode = 404)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static Response<T> Success(string successMessage, T data, int statusCode = 200)
        {
            return new Response<T> { Succeeded = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        /// <summary>
        /// public override string ToString() => JsonConvert.SerializeObject(this);
        /// </summary>
        public Response() { }

    }
}
