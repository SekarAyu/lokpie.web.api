using Lokpie.Repository.Models;
using QSI.Persistence.Query;
using QSI.Persistence.Repository;
using System.Collections.Generic;

namespace Lokpie.Repository
{
    public interface IReservationDao : BaseDao<Reservation, long>
    {
        IList<Reservation> GetReservation(ICondition condition);
    }
}