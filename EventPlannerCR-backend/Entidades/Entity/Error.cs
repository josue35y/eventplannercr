using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerCR_backend.Entidades
{
    public class Error
    {
        public enumErrores ErrorCode { get; set; }
        public string Message { get; set; }



        public static Error generarError(enumErrores ErrorCode, string message)
        {
            return new Error { ErrorCode = ErrorCode, Message = message };
        }
    }
}
