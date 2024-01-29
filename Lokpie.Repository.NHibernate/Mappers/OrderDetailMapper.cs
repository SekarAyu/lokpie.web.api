using QSI.ORM.NHibernate.Mapper;
using System;

namespace Lokpie.Repository.Mappers
{
    public class OrderDetailMapper : BaseAuditedEntityMapper<Models.OrderDetail, long>
    {
        public OrderDetailMapper() : base()
        {
            Table("lokpie_order_detail");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Price).Column("price").Nullable();
            Map(x => x.Qty).Column("qty").Nullable();
            Map(x => x.SubTotal).Column("subTotal").Nullable();
            Map(x => x.Note).Column("note").Nullable();
            References(x => x.Order).Column("order_id").ForeignKey().LazyLoad().Not.Nullable();
            References(x => x.Menu).Column("menu_id").ForeignKey().LazyLoad().Not.Nullable();
        }
    }
}
