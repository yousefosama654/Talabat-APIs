using System.Collections;
using System.Collections.Generic;

namespace Talabat_APIs.Errors
{
    public class ApiValidationErrorResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse(int status, IEnumerable<string>ERRORS, string message = null) :base(status,message)
        {
            this.Errors=ERRORS;
        }
    }
}
