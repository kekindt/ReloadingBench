using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ReloadingBench
{
    public class LotRepository : MongoBaseRepository<Lot>, IMongoRepository<Lot>, ILotRepository
    {
        protected const string collectionName = "lots";

        public LotRepository() : base(collectionName)
        {

        }

        public List<Lot> GetItems(Dictionary<string, object> filterValues)
        {
            FilterDefinitionBuilder<Lot> builder = Builders<Lot>.Filter;
            foreach (KeyValuePair<string, object> pair in filterValues)
            {
                if (pair.Value != null)
                {
                    switch (pair.Key)
                    {
                        case "CartridgeName":
                            builder.Eq(r => r.Cartridge.Name, pair.Value.ToString());
                            break;
                        case "LoadedDate":
                            builder.Eq(r => r.LoadedDate, pair.Value);
                            break;
                    }
                }
            }
            return base.GetItems(builder.ToBsonDocument());
        }
    }
}