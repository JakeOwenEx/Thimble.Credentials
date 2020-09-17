using Amazon;
using Amazon.DynamoDBv2;
using Microsoft.Extensions.Configuration;

namespace Thimble.UserAccount.AWS.AwsClient
{
    public class AwsService : IAwsService
    {
        private IConfiguration Configuration;
        
        public AwsService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public AmazonDynamoDBClient GetDynamoClient()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(Configuration["awsAccessKey"], Configuration["awsSecretKey"]); 
            return new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.EUWest1);
        }
    }
}