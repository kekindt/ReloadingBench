using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ReloadingBench
{
    public class PowderRepository : MongoBaseRepository<Powder>, IMongoRepository<Powder>, IPowderRepository
    {
        protected const string collectionName = "powders";

        public PowderRepository() : base(collectionName)
        {

        }

        public List<Powder> GetItems(Dictionary<string, object> filterValues)
        {
            FilterDefinitionBuilder<Powder> builder = Builders<Powder>.Filter;
            foreach (KeyValuePair<string, object> pair in filterValues)
            {
                if (pair.Value != null)
                {
                    switch (pair.Key)
                    {
                        case "Name":
                            builder.Eq(r => r.Name, pair.Value.ToString());
                            break;
                        case "Brand":
                            builder.Eq(r => r.Brand, pair.Value.ToString());
                            break;
                    }
                }
            }
            return base.GetItems(builder.ToBsonDocument());
        }
    }
}