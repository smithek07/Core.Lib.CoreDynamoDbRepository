using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Core.Lib.CoreDynamoDbRepository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Lib.CoreDynamoDbRepository.Core
{
    public class DynamoDbRepository<T> : IDynamoDbRepository<T> where T : new()
    {
        private readonly IDynamoDBContext dynamoDBContext;

        /// <summary>
        /// Create new instance of the <see cref=" DynamoDbRepository{T}"/>
        /// </summary>
        /// <param name="dynamoDbTableName"></param>
        public DynamoDbRepository(string dynamoDbTableName) : this(dynamoDbTableName, new AmazonDynamoDBClient())
        {
        }

        /// <summary>
        /// Create new instance of the <see cref=" DynamoDbRepository{T}"/>
        /// </summary>
        /// <param name="dynamoDbTableName"></param>
        /// <param name="dynamoDBClient"></param>
        public DynamoDbRepository(string dynamoDbTableName, IAmazonDynamoDB dynamoDBClient)
        {
            if(!string.IsNullOrEmpty(dynamoDbTableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(T)] =
                    new Amazon.Util.TypeMapping(typeof(T), dynamoDbTableName);
            }
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            dynamoDBContext = new DynamoDBContext(dynamoDBClient, config);
        }

        public async Task CreateOrUpdateAsync(T obj)
        {
            await dynamoDBContext.SaveAsync(obj);
        }

        public async Task DeleteAsync(T obj)
        {
            await dynamoDBContext.DeleteAsync(obj);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await dynamoDBContext.ScanAsync<T>(null).GetRemainingAsync();
        }

        public async Task<T> GetAsync(string key)
        {
            return await dynamoDBContext.LoadAsync<T>(key);
        }
    }
}
