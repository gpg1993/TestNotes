using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacon.ServiceDefine1.Dto;
using Bacon.ServiceDefine1.Entity;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Bacon.Service1.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _dbContext;
        private readonly ILogger _log;

        public UserRepository(MongoDbContext dbContext,ILoggerProvider loggerProvider)
        {
            _dbContext = dbContext;
            _log = loggerProvider.CreateLogger(nameof(UserRepository));
        }

        public async Task AddUserAsync(UserEntity info)
        {
            var user = await _dbContext.UserCollection.FindAsync(x => x.id == info.id);
            if (user.Current==null)
            {
                await _dbContext.UserCollection.InsertOneAsync(info);

                _log.LogInformation($"{info.ObjectId}user is created");
            }
        }

        public async Task<long> DeleteUserAsync(string id)
        {
            int idInt = Convert.ToInt32(id);
            var filterDefine = MongoDB.Driver.Builders<UserEntity>.Filter.Where(x => x.id == idInt);
            var result = await _dbContext.UserCollection.DeleteOneAsync(filterDefine);
            if (result.IsAcknowledged)
            {
                _log.LogError($"{id}is not deleted");
                return 0;
            }
            return result.DeletedCount;

        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
        {
            var filterDefine = MongoDB.Driver.Builders<UserEntity>.Filter.Where(x => x.id != 0);
            var  result = await _dbContext.UserCollection.FindAsync(filterDefine);
            return result.Current;
        }

        public async Task<UserEntity> GetUser(string id)
        {
            int idInt = Convert.ToInt32(id);
            var filterDefine = MongoDB.Driver.Builders<UserEntity>.Filter.Where(x => x.id == idInt);
            var result       = await _dbContext.UserCollection.FindAsync(filterDefine);
            return result.Current.FirstOrDefault();
        }

        public async Task<BsonValue> UpdateUserInfoAsync(UserEntity info)
        {
            var filterDefinition = MongoDB.Driver.Builders<UserEntity>.Filter.Where(x => x.id == info.id);
            var UpdateDefinition = MongoDB.Driver.Builders<UserEntity>.Update
                .Set(a => a.Account, info.Account)
                .Set(a => a.RealName, info.RealName)
                .Set(a => a.NickName, info.NickName)
                .Set(a => a.HeadIcon, info.HeadIcon)
                .Set(a => a.Gender, info.Gender)
                .Set(a => a.Birthday, info.Birthday)
                .Set(a => a.MobilePhone, info.MobilePhone);
            var updateResult = await _dbContext.UserCollection.UpdateOneAsync(filterDefinition, UpdateDefinition);
            if (updateResult.IsAcknowledged)
            {
                _log.LogError($"{info.id}is not deleted");
            }
            return updateResult.UpsertedId;
        }

    }
}
