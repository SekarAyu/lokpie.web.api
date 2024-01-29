using Lokpie.Common.Enums;

namespace Lokpie.Common.Queries
{
    public class FoodNBevQuery
    {
        public long? tenantId { get; set; }
        public ItemType? type { get; set; }
        public ItemCategory? category { get; set; }
        public ItemStatus? Status { get; set; }
    }
}
