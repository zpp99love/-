namespace HostedService
{
    public class ExceptionHostedService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //从NET6开始，当托管服务发送未处理的异常就会自动停止并退出，
            //可以设置Ingore，不推荐，因为异常应该重视，建议try包裹并记录异常
            try
            {
                string message = null;
                message.ToString();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
      
        }
    }
}
