using Lokpie.Common.Enums;
using System.Collections.Generic;

namespace Lokpie.Common.Commands
{
    public class AddNewOrderCommand
    {
        public long TableId { get; set; }
        public string UserName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IList<OrderDetailCommand> OrderDetails { get; set; }
    }

    public class OrderDetailCommand
    {
        public long MenuId { get; set; }
        public string Note { get; set; }
        public long Price { get; set; }
        public long Qty { get; set; }
        public long SubTotal { get; set; }
    }
}
