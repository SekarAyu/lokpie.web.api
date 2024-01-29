using Lokpie.Common.Enums;
using QSI.Persistence.Model;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class Reservation : AuditedEntity<long>
    {
        public virtual TenantTable Table { get; set; }
        public virtual long OrderId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ReservationName { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual string Note { get; set; }
        public virtual long PeopleQty { get; set; }
        public virtual ReservationStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                   Id == reservation.Id &&
                   Actor == reservation.Actor &&
                   Timestamp == reservation.Timestamp &&
                   EqualityComparer<TenantTable>.Default.Equals(Table, reservation.Table) &&
                   OrderId == reservation.OrderId &&
                   UserName == reservation.UserName &&
                   ReservationName == reservation.ReservationName &&
                   OrderDate == reservation.OrderDate &&
                   Note == reservation.Note &&
                   PeopleQty == reservation.PeopleQty &&
                   Status == reservation.Status;
        }

        public override int GetHashCode()
        {
            int hashCode = 1038380334;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TenantTable>.Default.GetHashCode(Table);
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ReservationName);
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            hashCode = hashCode * -1521134295 + PeopleQty.GetHashCode();
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            return hashCode;
        }
    }
}
