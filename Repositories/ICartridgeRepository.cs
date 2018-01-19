using System.Collections.Generic;

namespace ReloadingBench
{
    public interface ICartridgeRepository : IMongoRepository<Cartridge>
    {
        List<Cartridge> GetItems(Dictionary<string, object> filterValues);
    }
}