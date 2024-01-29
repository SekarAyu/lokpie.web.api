using Lokpie.Common.Enums;
using System;

namespace Lokpie.Common.Queries
{
    public class TableQuery
    {
        public DateTime? AvailableDate { get; set; }
        public TableStatus? Status { get; set; }
    }
}
