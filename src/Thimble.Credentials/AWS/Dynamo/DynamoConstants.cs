namespace Thimble.Credentials.AWS.Dynamo
{
    public static class DynamoConstants
    {
        public const string CREDENTIALS_TABLE = "Thimble.Credentials";
        public const string UserIdKey = "UserId";
        public const string EmailKey = "Email";
        public const string PasswordHashKey = "PasswordHash";
    }
}