using Lokpie.Repository.Models;
using QSI.Persistence.Query;
using QSI.Persistence.Repository;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface ITenantTableDao : BaseDao<TenantTable, long>
    {
        IList<TenantTable> GetTenantTable(ICondition condition);
    }
}