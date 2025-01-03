using ConsoleAppSquareMaster.Model;
using ConsoleAppSquareMaster.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Helper
{
    public class MongoDBHelper
    {
        private readonly IMongoCollection<WorldModel> _worldCollection;
        private readonly IMongoCollection<WorldResult> _resultCollection;

        public MongoDBHelper()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Worlds");
            _worldCollection = database.GetCollection<WorldModel>("WorldModels");
            _resultCollection = database.GetCollection<WorldResult>("WorldResults");
        }

        public async Task SaveWorldAsync(WorldModel world)
        {
            await _worldCollection.InsertOneAsync(world);
        }

        public async Task SaveWorldResultAsync(WorldResult result)
        {
            await _resultCollection.InsertOneAsync(result);
        }
        public async Task<List<WorldResult>> GetAllWorldResultsAsync()
        {
            return await _resultCollection.Find(_ => true).ToListAsync();
        }
    }
}
