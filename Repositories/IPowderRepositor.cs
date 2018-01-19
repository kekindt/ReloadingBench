using System.Collections.Generic;

namespace ReloadingBench
{
    public interface IPowderRepository : IMongoRepository<Powder>
    {
        List<Powder> GetItems(Dictionary<string, object> filterValues);
    }
}