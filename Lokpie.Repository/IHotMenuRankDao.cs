using Lokpie.Repository.Models;
using QSI.Persistence.Query;
using QSI.Persistence.Repository;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface IHotMenuRankDao : BaseDao<HotMenuRank, long>
    {
        IList<HotMenuRank> GetHotMenuRank(ICondition condition);
    }
}