using Quartz;
using System.Threading.Tasks;

namespace Lokpie.Service.Quartz
{
    [DisallowConcurrentExecution]
    public class HotMenuRankJob : IJob
    {
        private readonly IOrderService orderService;

        public HotMenuRankJob(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            orderService.SetRankOrder();
            return Task.CompletedTask;
        }
    }
}
