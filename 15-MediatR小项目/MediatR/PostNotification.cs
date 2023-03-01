using MediatR;

namespace MediatRTest
{
    /// <summary>
    /// 传输的数据类型类，因为消息在传递的过程中不会被修改，建议声明为record类型
    /// </summary>
    /// <param name="Body"></param>
    public record PostNotification(string Body) : INotification;
}
