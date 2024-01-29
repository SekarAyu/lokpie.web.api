using Lokpie.Common.Enums;
using QSI.Persistence.Model;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class Order : AuditedEntity<long>
    {
        public virtual TenantTable Table { get; set; }
        public virtual string UserName { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual long TotalPrice { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual IList<OrderDetail> OrderDetails { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Id == order.Id &&
                   Actor == order.Actor &&
                   Timestamp == order.Timestamp &&
                   EqualityComparer<TenantTable>.Default.Equals(Table, order.Table) &&
                   UserName == order.UserName &&
                   OrderDate == order.OrderDate &&
                   TotalPrice == order.TotalPrice &&
                   Status == order.Status &&
                   PaymentMethod == order.PaymentMethod &&
                   EqualityComparer<IList<OrderDetail>>.Default.Equals(OrderDetails, order.OrderDetails);
        }

        public override int GetHashCode()
        {
            int hashCode = -1573968116;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TenantTable>.Default.GetHashCode(Table);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + PaymentMethod.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<OrderDetail>>.Default.GetHashCode(OrderDetails);
            return hashCode;
        }
    }
}
