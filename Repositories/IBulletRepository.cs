using System.Collections.Generic;

namespace ReloadingBench
{
    public interface IBulletRepository : IMongoRepository<Bullet>
    {
        List<Bullet> GetItems(Dictionary<string, object> filterValues);
    }
}