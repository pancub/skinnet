namespace API.Errors
{
    public class ApiValidationError : ApiResponse
    {
        public ApiValidationError() : base(400)
        {
            
        }
    }
}