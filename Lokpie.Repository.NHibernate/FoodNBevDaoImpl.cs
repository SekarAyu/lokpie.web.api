using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class FoodNBevDaoImpl : BaseDaoNHibernate<FoodNBev, long>, IFoodNBevDao
    {
        public IList<FoodNBev> GetFoodNBev(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<FoodNBev>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .List<FoodNBev>();
        }
    }
}
