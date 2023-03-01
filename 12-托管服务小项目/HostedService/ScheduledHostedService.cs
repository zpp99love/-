namespace HostedService
{
    public class ScheduledHostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //程序终止的时候循环也停止
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("定时输出");

                await Task.Delay(3000);
            }

            
        }
    }
}
