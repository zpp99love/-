using MediatRTest;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IMediator mediator;
        public WeatherForecastController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("Publish")]
        public void Publish()
        {
            //Send()�Ƿ���һ��һ��Ϣ��Publish����һ�Զ���Ϣ
            mediator.Publish(new PostNotification("���ѽ��"+ DateTime.Now));
        }
    }
}