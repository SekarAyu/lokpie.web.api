using Lokpie.Common.Enums;
using System;

namespace Lokpie.Common.Dtos
{
    public class OrderDto
    {
        public long Id { get; set; }
        public int TableNo { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public long TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class OrderDetailDto
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public string Note { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }
        public long SubTotal { get; set; }
    }
}
