using Amazon.DynamoDBv2;

namespace Thimble.UserAccount.AWS.AwsClient
{
    public interface IAwsService
    {
        AmazonDynamoDBClient GetDynamoClient();
    }
}