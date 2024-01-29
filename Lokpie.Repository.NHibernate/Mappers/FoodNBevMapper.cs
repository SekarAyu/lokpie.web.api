using Lokpie.Common.Enums;
using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class FoodNBevMapper : BaseAuditedEntityMapper<Models.FoodNBev, long>
    {
        public FoodNBevMapper() : base()
        {
            Table("lokpie_food_bev");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Column("name").Nullable();
            Map(x => x.Description).Column("description").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.Price).Column("price").Nullable();
            Map(x => x.Photo).Column("photo").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.FileName).Column("fileName").Nullable();
            Map(x => x.Stock).Column("stock").Nullable();
            Map(x => x.Discount).Column("discount").Nullable();
            Map(x => x.Type).Column("type").CustomType<ItemType>().Nullable();
            Map(x => x.Status).Column("status").CustomType<ItemStatus>().Nullable();
            Map(x => x.Category).Column("category").CustomType<ItemCategory>().Nullable();
            Map(x => x.IsDeleted).Column("isDeleted").Nullable();
            References(x => x.Tenant).Column("tenant_id").ForeignKey().LazyLoad().Not.Nullable();
            HasMany(x => x.OrderDetails).KeyColumn("menu_id").LazyLoad();
        }
    }
}
