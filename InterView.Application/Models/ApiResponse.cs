using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InterView.Application.Models
{
    public abstract class ApiResponseAbs
    {
        public static readonly string SuccessfulMessage = "Transaction Successful";
        public static readonly string ErrorMessage = "System Error";

        public bool isSuccess { get; set; }
        public HttpStatusCode resultCode { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }

    public abstract class ApiResponse : ApiResponseAbs
    {
        public ApiResponse(bool statusVal, HttpStatusCode resultCodeVal, string messageVal, string tokenVal, object dataVal)
        {
            isSuccess = statusVal;
            resultCode = resultCodeVal;
            message = messageVal;
            token = tokenVal;
            data = dataVal;
        }

        public object data { get; set; }
    }

    public class SuccessfulResponse : ApiResponse
    {
        public SuccessfulResponse(string message = "") : base(true, HttpStatusCode.OK, string.IsNullOrEmpty(message) ? SuccessfulMessage : message, string.Empty, null)
        {
        }

        public SuccessfulResponse(object data) : base(true, HttpStatusCode.OK, SuccessfulMessage, string.Empty, data)
        {
        }
        public SuccessfulResponse(string message, object data) : base(true, HttpStatusCode.OK, message, string.Empty, data)
        {
        }
    }

    public class ErrorResponse : ApiResponse
    {
        public ErrorResponse(string message = "", HttpStatusCode resultCode = HttpStatusCode.InternalServerError, object data = null) : base(false, resultCode, string.IsNullOrEmpty(message) ? ErrorMessage : message, string.Empty, data)
        {
        }

    }

    public class ApiResponse<T> : ApiResponseAbs where T : class
    {
        public ApiResponse(bool statusVal, HttpStatusCode resultCodeVal, string messageVal, string tokenVal, T dataVal)
        {
            isSuccess = statusVal;
            resultCode = resultCodeVal;
            message = messageVal;
            token = tokenVal;
            data = dataVal;
        }

        public T data { get; set; }
    }

    public class SuccessfulResponse<T> : ApiResponse<T> where T : class
    {
        public SuccessfulResponse(string message = "") : base(true, HttpStatusCode.OK, string.IsNullOrEmpty(message) ? SuccessfulMessage : message, string.Empty, null)
        {
        }

        public SuccessfulResponse(T data) : base(true, HttpStatusCode.OK, SuccessfulMessage, string.Empty, data)
        {
        }
        public SuccessfulResponse(string message, T data) : base(true, HttpStatusCode.OK, message, string.Empty, data)
        {
        }
    }

    public class ErrorResponse<T> : ApiResponse<T> where T : class
    {
        public ErrorResponse(string message = "", HttpStatusCode resultCode = HttpStatusCode.InternalServerError, T data = null) : base(false, resultCode, string.IsNullOrEmpty(message) ? ErrorMessage : message, string.Empty, data)
        {
        }

    }
}
