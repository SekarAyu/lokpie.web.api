using Lokpie.Common.Enums;
using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class TenantMapper : BaseAuditedEntityMapper<Models.Tenant, long>
    {
        public TenantMapper() : base()
        {
            Table("lokpie_tenant");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Column("Name").Nullable();
            Map(x => x.Address).Column("Address").Nullable();
            Map(x => x.Phone).Column("Phone").Nullable();
            Map(x => x.Email).Column("Email").Nullable();
            Map(x => x.Logo).Column("Logo").CustomType("StringClob").CustomSqlType("nvarchar(max)").Nullable();
            Map(x => x.LogoFileName).Column("LogoFileName").Nullable();
            Map(x => x.Type).Column("type").CustomType<RestoEnum>().Nullable();
            Map(x => x.IsDeleted).Column("isDeleted").Nullable();
            HasMany(x => x.Tables).KeyColumn("tenant_id").LazyLoad();
            HasMany(x => x.Menus).KeyColumn("tenant_id").LazyLoad();
        }
    }
}
