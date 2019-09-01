using Bacon.Service1.Util;
using Bacon.ServiceDefine1.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bacon.Service1.Data
{
    public class MongoDbContext
    {
        private readonly IMongoClient   _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbContext(IOptionsSnapshot<MongoAppSetting> optionsSnapshot)
        {
            if (_mongoClient == null)
            {
                _mongoClient = new MongoClient(optionsSnapshot.Value.MongoConn);
            }
            if (_mongoDatabase == null)
            {
                _mongoDatabase = _mongoClient.GetDatabase(optionsSnapshot.Value.MongoDbDatabase);
            }
        }

        private void CheckOrCreateCollection(string name)
        {
            var list = _mongoDatabase.ListCollections().ToList().Select(x => x["name"].AsString);
            if (!list.Contains(name))
            {
                _mongoDatabase.CreateCollection(name);
            }
        }

        public IMongoCollection<UserEntity> UserCollection
        {
            get
            {
                CheckOrCreateCollection("FriendRequestCollection");
                return _mongoDatabase.GetCollection<UserEntity>("FriendRequestCollection");
            }
        }
    }
}
