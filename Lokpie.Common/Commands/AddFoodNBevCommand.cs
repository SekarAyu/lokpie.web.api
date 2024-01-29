using Lokpie.Common.Enums;

namespace Lokpie.Common.Commands
{
    public class AddFoodNBevCommand
    {
        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string Photo { get; set; }
        public string FileName { get; set; }
        public int Stock { get; set; }
        public ItemType Type { get; set; }
        public ItemStatus Status { get; set; }
        public ItemCategory Category { get; set; }
        public long Discount { get; set; }
    }

    public class UpdateFoodNBevCommand : AddFoodNBevCommand
    {
        public long Id { get; set; }
    }
}
