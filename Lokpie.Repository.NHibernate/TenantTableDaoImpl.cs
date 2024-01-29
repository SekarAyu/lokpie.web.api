using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class TenantTableDaoImpl : BaseDaoNHibernate<TenantTable, long>, ITenantTableDao
    {
        public IList<TenantTable> GetTenantTable(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<TenantTable>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .List<TenantTable>();
        }
    }
}
