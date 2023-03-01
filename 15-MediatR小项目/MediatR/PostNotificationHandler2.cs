using MediatR;

namespace MediatRTest
{
    public class PostNotificationHandler2 : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            Console.WriteLine("PostNotificationHandler2222我也接收到：" + notification.Body);
        }
    }
}
