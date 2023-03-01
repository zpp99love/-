namespace HostedService
{
    public class ReadFileHostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("ReadFileHostedService托管服务启动");
            await Task.Delay(1000);
            string txt = await File.ReadAllTextAsync("C:\\Users\\张跑跑\\Desktop\\github\\NetCore\\12-托管服务\\HostedService\\file.txt");
            Console.WriteLine("文件读取完成");
            await Task.Delay(1000);
            Console.WriteLine(txt);
        }
    }
}
