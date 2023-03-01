using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var connFactory = new ConnectionFactory();
connFactory.HostName = "localhost";
connFactory.DispatchConsumersAsync = true;
string exchangeName = "myexchage";//交换机名称
string eventName = "myEvent";//routingKey值
var connection = connFactory.CreateConnection();//创建TCP连接
var channel = connection.CreateModel();//创建临时虚拟信道


channel.ExchangeDeclare(exchange: exchangeName, type: "direct");//声明交换机
string queueName = "myQueue";//队列名称
channel.QueueDeclare(queueName,durable:true,exclusive:false,autoDelete:false,arguments:null);//声明队列
channel.QueueBind(queueName,exchangeName,routingKey:eventName);//绑定队列


AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);//接收消息
consumer.Received += Consumer_Received; //不停接收
channel.BasicConsume(queueName,autoAck:false,consumer:consumer);//开始接收
Console.WriteLine("按回车退出");
Console.ReadLine();

async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
{
	try
	{
        byte[] bytes = @event.Body.ToArray();
        string text = Encoding.UTF8.GetString(bytes);
        Console.WriteLine(DateTime.Now + "收到消息：" + text);
        //DeliveryTag是消息编号，队列在给消息的时候会标上一个编号
        channel.BasicAck(@event.DeliveryTag, multiple: false);

        await Task.Delay(2000);
    }
	catch (Exception ex)
	{

        Console.WriteLine(ex);
        channel.BasicReject(@event.DeliveryTag,true);//消息处理失败，拒绝。要求队列重新发送该消息
	}
}