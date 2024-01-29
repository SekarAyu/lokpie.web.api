using Lokpie.Common.Enums;
using System.Collections.Generic;

namespace Lokpie.Common.Dtos
{
    public class TableDto
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public ReservationType ReservationType { get; set; }
        public IList<string> TableReservationDate { get; set; }
    }
    
    public class TableDetailDto
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string AreaName { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public ReservationType ReservationType { get; set; }
        public string QrCode { get; set; }
        public string QrCodeFileName { get; set; }
        public string TablePhoto { get; set; }
        public string TablePhotoFileName { get; set; }
        public IList<string> TableReservationDate { get; set; }
    }
}
