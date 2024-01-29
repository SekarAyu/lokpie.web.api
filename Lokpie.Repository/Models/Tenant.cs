using Lokpie.Common.Enums;
using QSI.Persistence.Model;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class Tenant : AuditedEntity<long>
    {
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Logo { get; set; }
        public virtual string LogoFileName { get; set; }
        public virtual RestoEnum Type { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual IList<TenantTable> Tables { get; set; }
        public virtual IList<FoodNBev> Menus { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tenant tenant &&
                   Id == tenant.Id &&
                   Actor == tenant.Actor &&
                   Timestamp == tenant.Timestamp &&
                   Name == tenant.Name &&
                   Address == tenant.Address &&
                   Phone == tenant.Phone &&
                   Email == tenant.Email &&
                   Logo == tenant.Logo &&
                   LogoFileName == tenant.LogoFileName &&
                   Type == tenant.Type &&
                   IsDeleted == tenant.IsDeleted &&
                   EqualityComparer<IList<TenantTable>>.Default.Equals(Tables, tenant.Tables) &&
                   EqualityComparer<IList<FoodNBev>>.Default.Equals(Menus, tenant.Menus);
        }

        public override int GetHashCode()
        {
            int hashCode = -1486824477;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Logo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LogoFileName);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + IsDeleted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<TenantTable>>.Default.GetHashCode(Tables);
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<FoodNBev>>.Default.GetHashCode(Menus);
            return hashCode;
        }
    }
}
