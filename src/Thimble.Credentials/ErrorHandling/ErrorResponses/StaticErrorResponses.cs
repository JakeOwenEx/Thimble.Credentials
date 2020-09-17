namespace Thimble.Credentials.ErrorHandling.ErrorResponses
{
    public static class StaticErrorResponses
    {
        public static readonly UserAccount.Middleware.ErrorHandling.ErrorResponses.BaseErrorResponse Unauthorized = new UserAccount.Middleware.ErrorHandling.ErrorResponses.BaseErrorResponse
        {
            StatusCode = 401,
            Message = "Invalid Username or Password"
        };
        
    }
}