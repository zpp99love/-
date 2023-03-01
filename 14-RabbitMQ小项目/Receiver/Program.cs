using RabbitMQ.Client;
using System.Text;

var connFactory = new ConnectionFactory();
connFactory.HostName = "localhost";
connFactory.DispatchConsumersAsync = true;
string exchangeName = "myexchage";//交换机名称
string eventName = "myEvent";//routingKey值
var connection = connFactory.CreateConnection();//创建TCP连接
while (true)
{
    using (var channel = connection.CreateModel())//创建临时虚拟信道
    {
        var properties = channel.CreateBasicProperties();//消息属性对象
        properties.DeliveryMode = 2; //消息是否持久化：1为不持久，2为持久。

        channel.ExchangeDeclare(exchange: exchangeName, type: "direct");//声明交换机
        byte[] body = Encoding.UTF8.GetBytes(DateTime.Now.ToString());//消息内容字节化
        channel.BasicPublish(exchange: exchangeName, routingKey: eventName, mandatory: true, basicProperties: properties, body: body);//发送消息(mandatory：没有匹配路由routingKey值时退还)
        Console.WriteLine("发布了消息：" + DateTime.Now.ToString());

        Thread.Sleep(1000);
    }
}



/*
  一个消息队列可以绑定多个routingKey
  生产者和消费者两边都要声明同一个交换机名称和routingKey名称
  生产者和消费者需要有一边为交换机和routingKey绑定队列
  队列在生产者或消费者哪边声明都可以，一般放在消费者这边，但需要消费者先启动，不然生产者提前发的信息都将不会被接收到，因为还没有队列来存 
 */