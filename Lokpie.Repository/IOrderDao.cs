using Lokpie.Repository.Models;
using QSI.Persistence.Query;
using QSI.Persistence.Repository;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface IOrderDao : BaseDao<Order, long>
    {
        IList<Order> GetOrder(ICondition condition);
    }
}