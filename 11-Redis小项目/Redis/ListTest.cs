using Newtonsoft.Json;
using ServiceStack.Redis;


namespace Redis
{
    public class ListTest
    {
        #region 基本语法
        //common();
        public void common()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379,null,2))
            {
                //删除当前数据库所有key-value
                //client.FlushDb(); 
                Console.WriteLine("删除完成");


                //从上往下添加
                var c = new User() { Id = 1, Name = "c" };
                client.AddItemToList("list", JsonConvert.SerializeObject(c));
                var c1 = new User() { Id = 2, Name = "c1" };
                client.AddItemToList("list", JsonConvert.SerializeObject(c1));
                var c2 = new User() { Id = 3, Name = "c2" };
                client.AddItemToList("list", JsonConvert.SerializeObject(c2));


                //追加
                var c3 = new User() { Id = 4, Name = "c3" };
                client.PushItemToList("list", JsonConvert.SerializeObject(c3));

                //插入最上面
                var c4 = new User() { Id = 5, Name = "c4" };
                client.PrependItemToList("list", JsonConvert.SerializeObject(c4));


                //返回并移除
                //从尾移除
                Console.WriteLine(client.RemoveEndFromList("list"));
                //从头移除
                Console.WriteLine(client.RemoveStartFromList("list"));


                //根据下标修改元素value
                client.SetItemInList("list", 0, "upupupup");


                



            }
        }
        #endregion




        #region 设置过期时间
        public void ExpireEntryAt()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 2))
            {
                
                client.ExpireEntryAt("list",DateTime.Now.AddSeconds(2));
                Console.WriteLine(client.GetListCount("list"));//5
                Task.Delay(4000).Wait();
                Console.WriteLine(client.GetListCount("list"));//0


                //获取当前list的过期时间
                Console.WriteLine("过期时间："+client.GetTimeToLive("list"));


            }
        }
        #endregion




        #region 批量操作
        public void more()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 2))
            {
                //批量添加
                client.AddRangeToList("more", new List<string>() { "001", "002", "003" });
                //批量查询（按下标范围）
                var mores = client.GetRangeFromList("more",0,1);
                foreach (var item in mores)
                {
                    Console.WriteLine(item);
                }

            }
        }
        #endregion



        #region 拓展（把A集合的尾部元素移除，再添加到B集合头部中）
        public void PopAndPushItemBetweenLists()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 2))
            {
                Console.WriteLine(client.PopAndPushItemBetweenLists("list","newlist"));
                Console.WriteLine(client.PopAndPushItemBetweenLists("list","newlist"));
            }
        }
        #endregion


    }
}
