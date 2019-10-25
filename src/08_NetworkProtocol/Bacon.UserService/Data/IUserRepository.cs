using Bacon.ServiceDefine1.Dto;
using Bacon.ServiceDefine1.Entity;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bacon.Service1.Data
{
    public interface IUserRepository
    {
        Task<BsonValue> UpdateUserInfoAsync(UserEntity info);
        Task AddUserAsync(UserEntity info);
        Task<long> DeleteUserAsync(string id);
        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> GetUser(string id);
    }
}
