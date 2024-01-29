using Lokpie.Repository.Models;
using NHibernate;
using NHibernate.Criterion;
using QSI.ORM.NHibernate.Repository;
using QSI.Persistence.Query;
using System.Collections.Generic;

namespace Lokpie.Repository.NHibernate
{
    public class ReservationDaoImpl : BaseDaoNHibernate<Reservation, long>, IReservationDao
    {
        public IList<Reservation> GetReservation(ICondition condition)
        {
            ICriteria criteria = Session.CreateCriteria<Reservation>();
            criteria = ((Condition<ICriteria, ICriterion>)condition).BuildCondition(criteria);
            return criteria
                .SetFetchMode("Table", FetchMode.Eager)
                .CreateAlias("Table", "Table")
                .List<Reservation>();
        }
    }
}
