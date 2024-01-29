using System;
using System.Collections.Generic;
using System.Text;

namespace Lokpie.Common.Dtos
{
    public class HotMenuRankDto
    {
        public DateTime RankDate { get; set; }
        public long FirstId { get; set; }
        public long FirstQty { get; set; }
        public long SecondId { get; set; }
        public long SecondQty { get; set; }
        public long ThirdId { get; set; }
        public long ThirdQty { get; set; }
    }
}
