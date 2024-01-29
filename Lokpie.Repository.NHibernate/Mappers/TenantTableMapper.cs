using Lokpie.Common.Enums;
using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class TenantTableMapper : BaseAuditedEntityMapper<Models.TenantTable, long>
    {
        public TenantTableMapper() : base()
        {
            Table("lokpie_tenant_table");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.AreaName).Column("areaName").Nullable();
            Map(x => x.Description).Column("description").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.OrderLink).Column("orderLink").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.Number).Column("number").Nullable();
            Map(x => x.Capacity).Column("capacity").Nullable();
            Map(x => x.Status).Column("status").CustomType<TableStatus>().Nullable();
            Map(x => x.ReservationType).Column("reservationType").CustomType<ReservationType>().Nullable();
            Map(x => x.QrCode).Column("qrCode").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.QrCodeFileName).Column("qrCodeFileName").Nullable();
            Map(x => x.TablePhoto).Column("tablePhoto").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.TablePhotoFileName).Column("tablePhotoFileName").Nullable();
            Map(x => x.IsDeleted).Column("IsDeleted").Nullable();
            Map(x => x.TableReservationDate).Column("tableReservationDate").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            References(x => x.Tenant).Column("tenant_id").ForeignKey().LazyLoad().Not.Nullable();
            HasMany(x => x.Orders).KeyColumn("table_id").LazyLoad();
            HasMany(x => x.Reservations).KeyColumn("table_id").LazyLoad();
        }
    }
}
