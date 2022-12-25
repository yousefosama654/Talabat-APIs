using System.Collections.Generic;

namespace Talabat_APIs.Errors
{
    public class ApiExceptionError:ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptionError(int status, string Details=null, string message = null) :base(status,message)
        {
            this.Details = Details;
        }
    }
}
