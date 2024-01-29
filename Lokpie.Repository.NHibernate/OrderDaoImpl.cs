using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class OrderDaoImpl : BaseDaoNHibernate<Models.Order, long>, IOrderDao
    {
        public IList<Models.Order> GetOrder(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<Models.Order>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .SetFetchMode("Table", FetchMode.Eager)
                .CreateAlias("Table", "Table")
                .List<Models.Order>();
        }
    }
}
