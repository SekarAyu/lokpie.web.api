using Lokpie.Common.Enums;

namespace Lokpie.Common.Commands
{
    public class AddTableCommand
    {
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
    }

    public class UpdateTableCommand : AddTableCommand
    {
        public long Id { get; set; }
    }
}
