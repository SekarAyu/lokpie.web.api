using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class HotMenuRankDaoImpl : BaseDaoNHibernate<HotMenuRank, long>, IHotMenuRankDao
    {
        public IList<HotMenuRank> GetHotMenuRank(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<HotMenuRank>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .List<HotMenuRank>();
        }
    }
}
