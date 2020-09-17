namespace Thimble.UserAccount.Middleware.ErrorHandling.ErrorResponses
{
    public static class StaticErrorResponses
    {
        public static readonly BaseErrorResponse Unauthorized = new BaseErrorResponse
        {
            StatusCode = 401,
            Message = "Invalid Username or Password"
        };
        
        public static readonly BaseErrorResponse UserNotRegistered = new BaseErrorResponse
        {
            StatusCode = 404,
            Message = "There is no account with the provided UserId"
        };
        
        public static readonly BaseErrorResponse InvalidKey = new BaseErrorResponse
        {
            StatusCode = 400,
            Message = "Key does not exist in users contact information"
        };
    }
}