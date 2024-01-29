using Lokpie.Common.Enums;
using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class ReservationMapper : BaseAuditedEntityMapper<Models.Reservation, long>
    {
        public ReservationMapper() : base()
        {
            Table("lokpie_reservation");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.UserName).Column("userName").Nullable();
            Map(x => x.ReservationName).Column("reservationName").Nullable();
            Map(x => x.OrderDate).Column("orderDate").Nullable();
            Map(x => x.OrderId).Column("orderId").Nullable();
            Map(x => x.Note).Column("note").Nullable();
            Map(x => x.PeopleQty).Column("peopleQty").Nullable();
            Map(x => x.Status).Column("status").CustomType<ReservationStatus>().Nullable();
            References(x => x.Table).Column("table_id").ForeignKey().LazyLoad().Not.Nullable();
        }
    }
}
