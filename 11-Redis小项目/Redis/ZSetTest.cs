using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    public class ZSetTest
    {
        #region 基本语法  自动去重而且多了一个权重score字段，默认小到大排序

        public void common()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 4))
            {
                Console.WriteLine("*************************************");
                //删除当前数据库所有key-value
                //client.FlushDb(); 
                Console.WriteLine("删除完成");

                //添加-当不给分数赋值，默认该字段值最大
                client.AddItemToSortedSet("zset","a");
                client.AddItemToSortedSet("zset","b",350);

            }
        }
        #endregion


        #region 批量

        public void more()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 4))
            {
                //批量新增
                client.AddRangeToSortedSet("zsetmore", new List<string>() { "z", "x" }, 100);
                client.AddRangeToSortedSet("zsetmore", new List<string>() { "m", "n" }, 200);
                

                //批量查找-按分数从小到大
                var zsetlist = client.GetAllItemsFromSortedSet("zsetmore");
                foreach (var item in zsetlist)
                {
                    Console.WriteLine(item);
                }


                Console.WriteLine("*************************************");
                //批量查找-按分数从大到小
                var zsetlist2 = client.GetAllItemsFromSortedSetDesc("zsetmore");
                foreach (var item in zsetlist2)
                {
                    Console.WriteLine(item);
                }



                Console.WriteLine("*************************************");
                //批量查找-根据下标按分数从小到大
                var zsetlist3 = client.GetRangeFromSortedSet("zsetmore",0,1);
                foreach (var item in zsetlist3)
                {
                    Console.WriteLine(item);
                }


                Console.WriteLine("*************************************");
                //批量查找-根据下标按分数从大到小
                var zsetlist4 = client.GetRangeFromSortedSetDesc("zsetmore",0,1);
                foreach (var item in zsetlist4)
                {
                    Console.WriteLine(item);
                }



                Console.WriteLine("*************************************");
                //批量查找-根据下标按分数返回且返回分数
                var zsetlist5 = client.GetRangeWithScoresFromSortedSet("zsetmore", 0, 1);
                foreach (var item in zsetlist5)
                {
                    Console.WriteLine(item.Key+"---"+item.Value);
                }

            }
        }
        #endregion





        #region 交集(把若干ZSet集合的交集给一个新ZSet，并且返回交集个数)

        public void StoreIntersectFromSortedSets()
        {
            using (IRedisClient client = new RedisClient("localhost", 6379, null, 4))
            {
                client.AddItemToSortedSet("zset6", "a", 10);
                client.AddItemToSortedSet("zset6", "b", 20);
                client.AddItemToSortedSet("zset6", "c", 30);
                client.AddItemToSortedSet("zset6", "d", 40);
                
                client.AddItemToSortedSet("zset7", "a", 30);
                client.AddItemToSortedSet("zset7", "b", 30);
                
                client.AddItemToSortedSet("zset8", "a", 10);

                var zsetxlistcount = client.StoreIntersectFromSortedSets("nextzsetx", "zset6", "zset7", "zset8");//但是分数很奇怪！！！
                Console.WriteLine(zsetxlistcount);



            }
        }
        #endregion
    }
}
