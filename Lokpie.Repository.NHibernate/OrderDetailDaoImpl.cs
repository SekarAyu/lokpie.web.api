using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class OrderDetailDaoImpl : BaseDaoNHibernate<OrderDetail, long>, IOrderDetailDao
    {
        public IList<OrderDetail> GetOrderDetail(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<OrderDetail>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .SetFetchMode("Menu", FetchMode.Eager)
                .CreateAlias("Menu", "Menu")
                .List<OrderDetail>();
        }
    }
}
