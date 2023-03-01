using MediatR;

namespace MediatRTest
{
    /// <summary>
    /// 侦听消息类
    /// </summary>
    public class PostNotificationHandler1 : NotificationHandler<PostNotification>//PostNotification：要侦听哪种类型的消息
    {
        //重写NotificationHandler类方法
        protected override void Handle(PostNotification notification)//notification：真正的消息对象
        {
            Console.WriteLine("PostNotificationHandler1111接收到：" + notification.Body);
        }
    }
}
