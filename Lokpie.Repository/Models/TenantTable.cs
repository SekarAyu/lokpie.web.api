using Lokpie.Common.Enums;
using QSI.Persistence.Model;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class TenantTable : AuditedEntity<long>
    {
        public virtual Tenant Tenant { get; set; }
        public virtual string AreaName { get; set; }
        public virtual string Description { get; set; }
        public virtual string OrderLink { get; set; }
        public virtual int Number { get; set; }
        public virtual int Capacity { get; set; }
        public virtual TableStatus Status { get; set; }
        public virtual ReservationType ReservationType { get; set; }
        public virtual string QrCode { get; set; }
        public virtual string QrCodeFileName { get; set; }
        public virtual string TablePhoto { get; set; }
        public virtual string TablePhotoFileName { get; set; }
        public virtual string TableReservationDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual IList<Order> Orders { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TenantTable table &&
                   Id == table.Id &&
                   Actor == table.Actor &&
                   Timestamp == table.Timestamp &&
                   EqualityComparer<Tenant>.Default.Equals(Tenant, table.Tenant) &&
                   AreaName == table.AreaName &&
                   Description == table.Description &&
                   OrderLink == table.OrderLink &&
                   Number == table.Number &&
                   Capacity == table.Capacity &&
                   Status == table.Status &&
                   ReservationType == table.ReservationType &&
                   QrCode == table.QrCode &&
                   QrCodeFileName == table.QrCodeFileName &&
                   TablePhoto == table.TablePhoto &&
                   TablePhotoFileName == table.TablePhotoFileName &&
                   TableReservationDate == table.TableReservationDate &&
                   IsDeleted == table.IsDeleted &&
                   EqualityComparer<IList<Order>>.Default.Equals(Orders, table.Orders) &&
                   EqualityComparer<IList<Reservation>>.Default.Equals(Reservations, table.Reservations);
        }

        public override int GetHashCode()
        {
            int hashCode = 914864259;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Tenant>.Default.GetHashCode(Tenant);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AreaName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderLink);
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            hashCode = hashCode * -1521134295 + Capacity.GetHashCode();
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + ReservationType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(QrCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(QrCodeFileName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TablePhoto);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TablePhotoFileName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TableReservationDate);
            hashCode = hashCode * -1521134295 + IsDeleted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<Order>>.Default.GetHashCode(Orders);
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<Reservation>>.Default.GetHashCode(Reservations);
            return hashCode;
        }
    }
}
