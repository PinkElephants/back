using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hackinder.Entities;
using MongoDB.Driver;

namespace Hackinder.DB
{
    public class DbConnector
    {
        IMongoClient _client;
        protected static IMongoDatabase _database;


        public DbConnector(string connectionString)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("hackinder");
        }

        public IMongoDatabase GetDB()
        {
            return _database;
        }

        public IMongoCollection<Man> Mans { get { return _database.GetCollection<Man>("Mans"); } }
        public IMongoCollection<Skill> Skills { get { return _database.GetCollection<Skill>("Skills"); } }
    }
}
