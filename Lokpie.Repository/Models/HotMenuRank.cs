using QSI.Persistence.Model;
using System;
using System.Collections.Generic;

namespace Lokpie.Repository.Models
{
    public class HotMenuRank : AuditedEntity<long>
    {
        public virtual DateTime RankDate { get; set; }
        public virtual long FirstId { get; set; }
        public virtual long FirstQty { get; set; }
        public virtual long SecondId { get; set; }
        public virtual long SecondQty { get; set; }
        public virtual long ThirdId { get; set; }
        public virtual long ThirdQty { get; set; }

        public override bool Equals(object obj)
        {
            return obj is HotMenuRank rank &&
                   Id == rank.Id &&
                   Actor == rank.Actor &&
                   Timestamp == rank.Timestamp &&
                   RankDate == rank.RankDate &&
                   FirstId == rank.FirstId &&
                   FirstQty == rank.FirstQty &&
                   SecondId == rank.SecondId &&
                   SecondQty == rank.SecondQty &&
                   ThirdId == rank.ThirdId &&
                   ThirdQty == rank.ThirdQty;
        }

        public override int GetHashCode()
        {
            int hashCode = 1100672623;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Actor);
            hashCode = hashCode * -1521134295 + Timestamp.GetHashCode();
            hashCode = hashCode * -1521134295 + RankDate.GetHashCode();
            hashCode = hashCode * -1521134295 + FirstId.GetHashCode();
            hashCode = hashCode * -1521134295 + FirstQty.GetHashCode();
            hashCode = hashCode * -1521134295 + SecondId.GetHashCode();
            hashCode = hashCode * -1521134295 + SecondQty.GetHashCode();
            hashCode = hashCode * -1521134295 + ThirdId.GetHashCode();
            hashCode = hashCode * -1521134295 + ThirdQty.GetHashCode();
            return hashCode;
        }
    }
}
