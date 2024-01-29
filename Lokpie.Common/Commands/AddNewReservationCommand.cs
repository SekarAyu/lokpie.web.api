using System;
using System.Collections.Generic;

namespace Lokpie.Common.Commands
{
    public class AddNewReservationCommand
    {
        public long TableId { get; set; }
        public string UserName { get; set; }
        public string ReservationName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public long PeopleQty { get; set; }
        public IList<OrderDetailCommand> OrderDetails { get; set; }
    }
}
