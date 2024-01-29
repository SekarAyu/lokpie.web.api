using Lokpie.Common.Enums;
using System;

namespace Lokpie.Common.Queries
{
    public class ReservationQuery
    {
        public ReservationStatus? status { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string username { get; set; }
    }
}
