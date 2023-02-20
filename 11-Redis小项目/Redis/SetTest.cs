using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    //set 也是一个无序集合，能实现自动去重
    public class SetTest
    {
        #region 基本语法
        //common();
        public void common()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 3))
            {
                Console.WriteLine("*************************************");
                //删除当前数据库所有key-value
                //client.FlushDb(); 
                Console.WriteLine("删除完成");


                //set是无序添加
                var c = new User() { Id = 1, Name = "张三" };
                client.AddItemToSet("set", JsonConvert.SerializeObject(c));
                client.AddItemToSet("set", JsonConvert.SerializeObject(c));
                var c1 = new User() { Id = 2, Name = "李四" };
                client.AddItemToSet("set", JsonConvert.SerializeObject(c1));
                var c2 = new User() { Id = 3, Name = "王五" };
                client.AddItemToSet("set", JsonConvert.SerializeObject(c2));

                //长度
                Console.WriteLine(client.GetSetCount("set"));


                //删除
                //从一端删除
                Console.WriteLine(client.PopItemFromSet("set"));
                //删除指定value无返回值
                client.RemoveItemFromSet("set","张三");
                //元素从ASet中移除到BSet中
                client.MoveBetweenSets("set", "newset", "李四");
                
            
            
            }
        }
        #endregion


        #region 批量
        //common();
        public void more()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 3))
            {
                Console.WriteLine("*************************************");
                //批量添加
                client.AddRangeToSet("set1",new List<string>() { "001","002","003","003"});
                //长度
                Console.WriteLine(client.GetSetCount("set1"));


                //批量查询
                var sets = client.GetAllItemsFromSet("set1");
                foreach (var item in sets)
                {
                    Console.WriteLine(item);
                }

            }
        }
        #endregion


        #region 随机查询

        public void GetRandomItemFromSet()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 3))
            {
                Console.WriteLine("*************************************");
                //随机查询
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(client.GetRandomItemFromSet("set1"));
                }

            }
        }
        #endregion





        #region 交并集

        public void GetIntersectFromSets()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 3))
            {
                client.AddRangeToSet("qq1", new List<string>(){ "1", "2" });
                client.AddRangeToSet("qq2", new List<string>(){ "1" });

                //交集（我有两个qq号，有一个共同好友，如何得到？）
                var jjset = client.GetIntersectFromSets("qq1","qq2");
                foreach (var item in jjset)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("*************************************");
                //并集
                var bjset = client.GetUnionFromSets("qq1", "qq2");
                foreach (var item in bjset)
                {
                    Console.WriteLine(item);
                }
            }
        }
        #endregion









    }
}
