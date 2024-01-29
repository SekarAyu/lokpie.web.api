using Lokpie.Common.Enums;
using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class OrderMapper : BaseAuditedEntityMapper<Models.Order, long>
    {
        public OrderMapper() : base()
        {
            Table("lokpie_order");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.UserName).Column("UserName").Nullable();
            Map(x => x.OrderDate).Column("OrderDate").Nullable();
            Map(x => x.TotalPrice).Column("TotalPrice").Nullable();
            Map(x => x.Status).Column("status").CustomType<OrderStatus>().Nullable();
            Map(x => x.PaymentMethod).Column("paymentMethod").CustomType<PaymentMethod>().Nullable();
            References(x => x.Table).Column("table_id").ForeignKey().LazyLoad().Not.Nullable();
            HasMany(x => x.OrderDetails).KeyColumn("order_id").LazyLoad();
        }
    }
}
