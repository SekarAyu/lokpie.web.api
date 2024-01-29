using Lokpie.Common.Enums;

namespace Lokpie.Common.Dtos
{
    public class FoodNBevDto
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int Stock { get; set; }
        public ItemType Type { get; set; }
        public ItemStatus Status { get; set; }
        public ItemCategory Category { get; set; }
        public long Discount { get; set; }
    }

    public class BasePhotoDto
    {
        public long Id { get; set; }
        public string Photo { get; set; }
        public string FileName { get; set; }
    }

    public class FoodNBevDetailDto
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public int Stock { get; set; }
        public ItemType Type { get; set; }
        public ItemStatus Status { get; set; }
        public ItemCategory Category { get; set; }
        public long Discount { get; set; }
        public string Photo { get; set; }
        public string FileName { get; set; }
    }
}
