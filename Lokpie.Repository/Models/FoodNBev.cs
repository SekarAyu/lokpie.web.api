using Lokpie.Common.Enums;
using QSI.Persistence.Model;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class FoodNBev : AuditedEntity<long>
    {
        public virtual Tenant Tenant { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual long Price { get; set; }
        public virtual string Photo { get; set; }
        public virtual string FileName { get; set; }
        public virtual int Stock { get; set; }
        public virtual ItemType Type { get; set; }
        public virtual ItemStatus Status { get; set; }
        public virtual ItemCategory Category { get; set; }
        public virtual long Discount { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual IList<OrderDetail> OrderDetails { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FoodNBev bev &&
                   Id == bev.Id &&
                   Actor == bev.Actor &&
                   Timestamp == bev.Timestamp &&
                   EqualityComparer<Tenant>.Default.Equals(Tenant, bev.Tenant) &&
                   Name == bev.Name &&
                   Description == bev.Description &&
                   Price == bev.Price &&
                   Photo == bev.Photo &&
                   FileName == bev.FileName &&
                   Stock == bev.Stock &&
                   Type == bev.Type &&
                   Status == bev.Status &&
                   Category == bev.Category &&
                   Discount == bev.Discount &&
                   IsDeleted == bev.IsDeleted &&
                   EqualityComparer<IList<OrderDetail>>.Default.Equals(OrderDetails, bev.OrderDetails);
        }

        public override int GetHashCode()
        {
            int hashCode = -1894792473;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Tenant>.Default.GetHashCode(Tenant);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Photo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName);
            hashCode = hashCode * -1521134295 + Stock.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + Category.GetHashCode();
            hashCode = hashCode * -1521134295 + Discount.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDeleted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<OrderDetail>>.Default.GetHashCode(OrderDetails);
            return hashCode;
        }
    }
}
