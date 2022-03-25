using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Lib.CoreDynamoDbRepository.Interface
{
    public interface IDynamoDbRepository<T> where T : new ()
    {
        /// <summary>
        /// Fetch data from DynamoDB.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Data for <typeparamref name="T"/> from DynamoDB</returns>
        Task<T> GetAsync(string key);

        /// <summary>
        /// Fetch collection of data for from DynamoDB
        /// </summary>
        /// <returns>Collection of data for <typeparamref name="T"/> from DynamoDB</returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Insert or Update DynamoDB data.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Void Task</returns>
        Task CreateOrUpdateAsync(T obj);

        /// <summary>
        /// Delete a record from DynamoDB.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Void Task</returns>
        Task DeleteAsync(T obj);

    }
}
