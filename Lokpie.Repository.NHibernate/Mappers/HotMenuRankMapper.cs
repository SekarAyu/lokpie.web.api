using QSI.ORM.NHibernate.Mapper;

namespace Lokpie.Repository.Mappers
{
    public class HotMenuRankMapper : BaseAuditedEntityMapper<Models.HotMenuRank, long>
    {
        public HotMenuRankMapper() : base()
        {
            Table("lokpie_hot_menu_rank");

            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.RankDate).Column("RankDate").Nullable();
            Map(x => x.FirstId).Column("FirstId").Nullable();
            Map(x => x.FirstQty).Column("FirstQty").Nullable();
            Map(x => x.SecondId).Column("SecondId").Nullable();
            Map(x => x.SecondQty).Column("SecondQty").Nullable();
            Map(x => x.ThirdId).Column("ThirdId").Nullable();
            Map(x => x.ThirdQty).Column("ThirdQty").Nullable();
        }
    }
}
