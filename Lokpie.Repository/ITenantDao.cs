using Lokpie.Repository.Models;
using QSI.Persistence.Repository;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface ITenantDao : BaseDao<Tenant, long>
    {
        
    }
}