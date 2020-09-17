using Amazon.DynamoDBv2.DataModel;

namespace Thimble.Credentials.AWS.Dynamo.Credentials.Models
{
    [DynamoDBTable("Thimble.UserAccount.Password")]
    public class PasswordEntryDynamo
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
    }
}