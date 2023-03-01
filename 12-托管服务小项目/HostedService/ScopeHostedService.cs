namespace HostedService
{
    public class ScopeHostedService : BackgroundService
    {
        //手动一个和自己类一样生命周期的服务容器工厂（这样做的原因是依赖注入有生命周期大小的限制）
        //private readonly IServiceScopeFactory _serviceScopeFactory;//不保留，所以不用
        private readonly IServiceScope _serviceScope;

        public ScopeHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            //_serviceScopeFactory = serviceScopeFactory;
            _serviceScope = serviceScopeFactory.CreateScope();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //往容器注入服务
            var sumTest = _serviceScope.ServiceProvider.GetRequiredService<SumTest>();

            Console.WriteLine(sumTest.Add(5,6));

            return Task.CompletedTask;
        }

        //重写Dispose，在自己死亡的时候也将自己的容器工厂结束，其中的所有服务也被死亡。
        public override void Dispose()
        {
            _serviceScope.Dispose();
            base.Dispose();
        }


    }
}
