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
            //Send()是发布一对一消息，Publish发布一对多消息
            mediator.Publish(new PostNotification("你好呀！"+ DateTime.Now));
        }
    }
}