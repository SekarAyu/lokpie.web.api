using QSI.Persistence.Model;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class OrderDetail : AuditedEntity<long>
    {
        public virtual DateTime Timestamp { get; set; }
        public virtual Order Order { get; set; }
        public virtual FoodNBev Menu { get; set; }
        public virtual string Note { get; set; }
        public virtual long Price { get; set; }
        public virtual long Qty { get; set; }
        public virtual long SubTotal { get; set; }

        public override bool Equals(object obj)
        {
            return obj is OrderDetail detail &&
                   Id == detail.Id &&
                   Actor == detail.Actor &&
                   Timestamp == detail.Timestamp &&
                   EqualityComparer<Order>.Default.Equals(Order, detail.Order) &&
                   EqualityComparer<FoodNBev>.Default.Equals(Menu, detail.Menu) &&
                   Note == detail.Note &&
                   Price == detail.Price &&
                   Qty == detail.Qty &&
                   SubTotal == detail.SubTotal;
        }

        public override int GetHashCode()
        {
            int hashCode = 1282780189;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Order>.Default.GetHashCode(Order);
            hashCode = hashCode * -1521134295 + EqualityComparer<FoodNBev>.Default.GetHashCode(Menu);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Note);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + Qty.GetHashCode();
            hashCode = hashCode * -1521134295 + SubTotal.GetHashCode();
            return hashCode;
        }
    }
}
