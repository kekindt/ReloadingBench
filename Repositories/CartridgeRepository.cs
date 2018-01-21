using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;

namespace ReloadingBench
{
    public class CartridgeRepository : MongoBaseRepository<Cartridge>, IMongoRepository<Cartridge>, ICartridgeRepository
    {
        protected const string collectionName = "cartridges";

        public CartridgeRepository(IConfiguration configuration) : base(configuration, collectionName)
        {

        }

        public List<Cartridge> GetItems(Dictionary<string, object> filterValues)
        {
            FilterDefinitionBuilder<Cartridge> builder = Builders<Cartridge>.Filter;
            foreach (KeyValuePair<string, object> pair in filterValues)
            {
                if (pair.Value != null)
                {
                    switch (pair.Key)
                    {
                        case "Name":
                            builder.Eq(r => r.Name, pair.Value.ToString());
                            break;
                       /* case "Weight":
                            builder.Eq(r => r.Weight, (int) pair.Value);
                            break;
                        case "Brand":
                            builder.Eq(r => r.Brand, pair.Value.ToString());
                            break;*/
                    }
                }
            }
            return base.GetItems(builder.ToBsonDocument());
        }
    }
}