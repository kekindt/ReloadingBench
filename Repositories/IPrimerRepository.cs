using System.Collections.Generic;

namespace ReloadingBench
{
    public interface IPrimerRepository : IMongoRepository<Primer>
    {
        List<Primer> GetItems(Dictionary<string, object> filterValues);
    }
}