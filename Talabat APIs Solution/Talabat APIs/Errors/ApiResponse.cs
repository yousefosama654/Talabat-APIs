namespace Talabat_APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public ApiResponse(int status, string message = null)
        {
            this.StatusCode = status;
            this.message = message ?? GetDefaultMessageForStatus();
        }
        private string GetDefaultMessageForStatus()
        {
            switch (this.StatusCode)
            {
                case 400:
                    return "U Have Made a bad Request";
                    break;
                case 401:
                    return "U AREN'T Authorized";
                    break;
                case 404:
                    return "Not Found Sir";
                    break;
                case 500:
                    return "Server Validation Error will lead to career change ";
                    break;
                    default:
                    return null;
            }
        }
    }
}
