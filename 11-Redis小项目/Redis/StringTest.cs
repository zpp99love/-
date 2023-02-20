using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    public class StringTest
    {

        #region 基本语法
        //common();
        public void common()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {
                //删除当前数据库所有key-value
                //client.FlushDb();
                Console.WriteLine("删除完成");




                //新增
                client.Set<string>("name", "张三");
                //查询 一般不建议这个，结果会多加双引号，或者引入包执行JsonConvent.DeserializeObject<string>(v);
                string v = client.GetValue("name");
                Console.WriteLine(v); //"张三"


                //推荐，api帮我们做了反序列化
                string v1 = client.Get<string>("name");
                Console.WriteLine(v1);//张三
            }
        }
        #endregion



        #region 批量操作
        //more();
        public void more()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {
                var dics = new Dictionary<string, string>();
                dics.Add("id", "1");
                dics.Add("name", "李四");
                dics.Add("address", "深圳");
                //批量新增
                client.SetAll(dics);

                //批量查询
                var list = client.GetAll<string>(new string[] { "id", "name", "address" });
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }
        }
        #endregion



        #region 过期时间
        //timeout();
        public void timeout()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {
                client.Set<string>("seconds", "2023.02.18", TimeSpan.FromSeconds(5));

                Console.WriteLine(client.Get<string>("seconds"));
                Task.Delay(5000).Wait();
                //string类型如果读不到key则返回""
                Console.WriteLine(client.Get<string>("seconds"));


                //指定在某一时刻过期
                client.Set<string>("AddDays", "2023.02.18", DateTime.Now.AddDays(1));
            }
        }
        #endregion



        #region 追加value
        //AppendToValue();
        public void AppendToValue()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {

                client.Set<string>("msg", "you love");
                //在已有key-value追加
                client.AppendToValue("msg", "me");
                Console.WriteLine(client.Get<string>("msg"));


                //对于原来就不存在的直接会创建一个key-value
                client.AppendToValue("new", "me");
                Console.WriteLine(client.Get<string>("new"));
            }
        }
        #endregion



        #region 重新赋值
        //GetAndSetValue();
        public void GetAndSetValue()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {

                client.Set<string>("age", "18");
                string oldage = client.GetAndSetValue("age", "1000");//需要反序列化
                                                                     //旧值
                Console.WriteLine(JsonConvert.DeserializeObject<string>(oldage));
                //新值
                Console.WriteLine(client.Get<string>("age"));
            }
        }
        #endregion



        #region 自增自减
        //IncrementDecrement();
        public void IncrementDecrement()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {
                long a = client.IncrementValueBy("count+", 1);//Increment也可以
                Console.WriteLine(a);
                client.IncrementValueBy("count+", 100);
                Console.WriteLine(client.Get<int>("count+"));


                long b = client.Decrement("count-", 1);
                Console.WriteLine(b);
                client.Decrement("count-", 100);
                Console.WriteLine(client.Get<int>("count-"));

            }
        }
        #endregion



        #region set和add区别
        //addset();
        public void addset()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {
                //add：只是新增，返回bool值，若存在此key，则新增失败返回false，反之成功
                Console.WriteLine(client.Add<string>("aadd", "4399"));//true
                Console.WriteLine(client.Add<string>("aadd", "7k7k"));//false


                //set：直接替换，返回bool值
                Console.WriteLine(client.Set<string>("set", "4399"));//true
                Console.WriteLine(client.Set<string>("set", "7k7k"));//true

            }
        }
        #endregion



        #region 判断数据库是否存在此key
        //ContainsKey();
        public void ContainsKey()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {

                client.Set<string>("kkkk", "fadfaf");
                Console.WriteLine(client.ContainsKey("kkkk"));//true
                Console.WriteLine(client.ContainsKey("ffff"));//false
            }
        }
        #endregion



        #region 获取key-value类型
        //GetEntryType();
        public void GetEntryType()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379))
            {

                client.Set<string>("type", "sssss");
                Console.WriteLine(client.GetEntryType("type"));//String

                client.AddItemToList("list", "123");
                Console.WriteLine(client.GetEntryType("list"));//List

            }
        }
        #endregion


    }
}
