using Lokpie.Repository.Models;
using QSI.Persistence.Query;
using QSI.Persistence.Repository;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface IOrderDetailDao : BaseDao<OrderDetail, long>
    {
        IList<OrderDetail> GetOrderDetail(ICondition condition);
    }
}