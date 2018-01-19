using System.Collections.Generic;

namespace ReloadingBench
{
    public interface ILotRepository : IMongoRepository<Lot>
    {
        List<Lot> GetItems(Dictionary<string, object> filterValues);
    }
}