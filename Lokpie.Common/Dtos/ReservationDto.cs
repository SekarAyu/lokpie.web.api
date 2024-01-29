using Lokpie.Common.Enums;
using System;

namespace Lokpie.Common.Dtos
{
    public class ReservationDto
    {
        public virtual long Id { get; set; }
        public virtual long TableId { get; set; }
        public virtual int TableNumber { get; set; }
        public virtual string TableAreaName { get; set; }
        public virtual long OrderId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ReservationName { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual string Note { get; set; }
        public virtual long PeopleQty { get; set; }
        public virtual ReservationStatus Status { get; set; }
    }
}
