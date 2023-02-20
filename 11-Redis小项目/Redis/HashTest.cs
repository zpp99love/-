using ServiceStack.Redis;


namespace Redis
{
    /// <summary>
    /// Hash相比String的好处：存储用户信息：
    /// String：首先把对象做一个json序列化，然后存储到redis的string类型中，
    /// 如果我们需要改变这个对象其中某个属性，则需要读取该对象并反序列化
    /// 最后修改完成之后再序列化用string存进去，很不方便。
    /// </summary>
    public class HashTest
    {
        #region 基本语法
        public void common()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379,null,1))
            {

                //删除当前数据库所有key-value
                //client.FlushDb();
                Console.WriteLine("删除完成");

                string hashid = "stu";
                //新增
                client.SetEntryInHash(hashid,"name","王五");
                client.SetEntryInHash(hashid, "age", "18");
                //查询
                Console.WriteLine(client.GetValueFromHash(hashid,"name"));
                Console.WriteLine(client.GetValueFromHash(hashid, "age"));
                //删除
                client.RemoveEntryFromHash(hashid, "age");
            }
        }
        #endregion



        #region 批量操作
        public void more()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 1))
            {
                string hashid = "stu1";
                //批量新增
                Dictionary<string,string> stu1 = new Dictionary<string,string>();
                stu1.Add("name","小二");
                stu1.Add("age", "20");
                stu1.Add("address", "深圳");

                client.SetRangeInHash(hashid, stu1);

                //批量查询
                var dics = client.GetAllEntriesFromHash(hashid);
                foreach (var item in dics)
                {
                    Console.WriteLine(item.Key+":"+item.Value);
                }
              
            }
        }
        #endregion



        #region 判断哈希集合中某个key是否存在，存在则返回false，不存在则把k-v新增进去
        public void isExist()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 1))
            {
                string hashid = "stu1";

                Console.WriteLine(client.SetEntryInHashIfNotExists(hashid, "name", "小二"));
                Console.WriteLine(client.SetEntryInHashIfNotExists(hashid, "name", "小三"));
            }
        }
        #endregion



        #region 存储对象
        public void userinfo()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 1))
            {
                //新增
                client.StoreAsHash<User>(new User { Id = 1, Name = "uu", Address = "巴塞罗那" });
                //查询
                Console.WriteLine(client.GetFromHash<User>(1).Name);

            }
        }
        #endregion



        #region 读取Hash集合中k-v总个数、所有key、所有value、某个key是否存在
        public void total()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 1))
            {
                string hashid = "stu1";
                //Hash集合中k-v总个数
                Console.WriteLine(client.GetHashCount(hashid));

                //所有key
                List<string> keys =  client.GetHashKeys(hashid);
                foreach (var item in keys)
                {
                    Console.WriteLine(item);
                }

                //所有value
                List<string> values = client.GetHashValues(hashid);
                foreach (var item in values)
                {
                    Console.WriteLine(item);
                }


                //某个key是否存在
                Console.WriteLine(client.HashContainsEntry(hashid,"name"));
            }
        }
        #endregion



        #region 自增自减
        public void IncrementDecrement()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 1))
            {
                string hashid = "stu1";

                //自增
                client.IncrementValueInHash(hashid, "age", 1);
                Console.WriteLine(client.GetValueFromHash(hashid, "age"));
            }
        }
        #endregion

    }
}
