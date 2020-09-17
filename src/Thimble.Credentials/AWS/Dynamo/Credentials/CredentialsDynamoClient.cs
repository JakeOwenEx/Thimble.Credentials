using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Thimble.Credentials.AWS.Dynamo.Credentials.Exceptions;
using Thimble.Credentials.AWS.Dynamo.Credentials.Helpers;
using Thimble.Credentials.AWS.Dynamo.Credentials.Models;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Thimble.Credentials.Controllers.Models.Login;
using Thimble.UserAccount.AWS.AwsClient;

namespace Thimble.Credentials.AWS.Dynamo.Credentials
{
    public class CredentialsDynamoClient : ICredentialsDynamoClient
    {
        private readonly AmazonDynamoDBClient _dynamoClient;
        
        public CredentialsDynamoClient(
            IAwsService awsService)
        {
            _dynamoClient = awsService.GetDynamoClient();
        }

        public async Task<DeleteItemResponse> DeleteUser(string userId)
        {
            return await _dynamoClient.DeleteItemAsync(new DeleteItemRequest(DynamoConstants.CREDENTIALS_TABLE, 
               new Dictionary<string, AttributeValue>
               {
                   {DynamoConstants.UserIdKey, new AttributeValue(userId)}
               }));
        }

        public async Task CreatePassword(PasswordRequest passwordRequest)
        {
            await _dynamoClient.PutItemAsync(DynamoConstants.CREDENTIALS_TABLE, new Dictionary<string, AttributeValue>
            {
                {DynamoConstants.UserIdKey, new AttributeValue(passwordRequest.UserId)},
                {DynamoConstants.EmailKey, new AttributeValue(passwordRequest.Email)},
                {DynamoConstants.PasswordHashKey, new AttributeValue(
                    PasswordHashHelper.Generate(passwordRequest.Password))}
            });
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            var context = new DynamoDBContext(_dynamoClient);
            
            var scanConditions = new List<ScanCondition>
            {
                new ScanCondition("Email", ScanOperator.Equal, new object[]{loginRequest.Email})
            };
            var config = new DynamoDBOperationConfig()
            {
                OverrideTableName = DynamoConstants.CREDENTIALS_TABLE
            };

            var passwordResponse = await context.ScanAsync<PasswordEntryDynamo>(scanConditions, config).GetRemainingAsync();
            if(passwordResponse.Count == 0) throw new InvalidEmailOrPasswordException();
            return PasswordHashHelper.Verify(passwordResponse[0].PasswordHash, loginRequest.Password);
        }
    }
}