// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Redis;
using Redis.record;
using ServiceStack;
using ServiceStack.Redis;
using System.Text.Json.Serialization;

//Console.WriteLine("Hello, World!");

#region String类型测试
StringTest stringTest = new StringTest();
//基本语法
//stringTest.common();
//批量操作
//stringTest.more();
//过期时间
//stringTest.timeout();
//追加value
//stringTest.AppendToValue();
//重新赋值
//stringTest.GetAndSetValue();
//自增自减
//stringTest.IncrementDecrement();
//set和add区别
//stringTest.addset();
//判断数据库是否存在此key
//stringTest.ContainsKey();
//获取key-value类型
//stringTest.GetEntryType();
#endregion 


#region Hash类型测试
HashTest hashTest = new HashTest();
//基本语法
//hashTest.common();
//批量操作
//hashTest.more();
//判断哈希集合中某个key是否存在，存在则返回false，不存在则把k-v新增进去
//hashTest.isExist();
//存储对象
//hashTest.userinfo();
//读取Hash集合中k-v总个数、所有key、所有value、某个key是否存在
//hashTest.total();
//自增自减
//hashTest.IncrementDecrement();
#endregion 


#region List类型测试
ListTest listTest = new ListTest();
//基本语法
//listTest.common();
//过期时间
//listTest.ExpireEntryAt();
//批量操作
//listTest.more();
//拓展（把A集合的尾部元素移除，再添加到B集合头部中）
//listTest.PopAndPushItemBetweenLists();
#endregion 


#region Set类型测试

SetTest setTest = new SetTest();
//基本语法
//setTest.common();
//批量操作
//setTest.more();
//随机查询
//setTest.GetRandomItemFromSet();
//交并集
//setTest.GetIntersectFromSets();

#endregion


#region ZSet类型测试

ZSetTest zsetTest = new ZSetTest();
//基本语法
//zsetTest.common();
//批量
//zsetTest.more();    
//交集
//zsetTest.StoreIntersectFromSortedSets();

#endregion


#region record 测试
/*

//测试record
User user = new User() { Id=5,Name="二麻子",Address="罗湖"};
User user2 = new User() { Id = 5, Name = "二麻子", Address = "罗湖" };
Console.WriteLine(user.ToString());//不重写是Redis.User，重写是Person { Id = 1, Name = p1, Address = ppp }
Console.WriteLine(user==user2);


//测试record
//全部字段都在构造函数赋值的写法
Person p1 = new Person(1,"p1","ppp");
Person p2 = new Person(1, "p1", "ppp");
Person p3 = new Person(3, "p2", "bbbb");
Console.WriteLine(p1.ToString());//不重写也是Person { Id = 1, Name = p1, Address = ppp }，自带封装了ToString()
Console.WriteLine(p1 == p2);//t
Console.WriteLine(p3 == p2);//f
Console.WriteLine(Object.ReferenceEquals(p1,p2));//f，ReferenceEquals测试是否指向同一地址



//部分字段构造函数部分字段其他地方赋值的写法（单独拿出来的字段只是多加了一个可写的功能，不影响ToString()、Equals()忘记它们
Student s1 = new Student(1){ Name = "s1" };//第一种写法
//s1.Id = 2;不行
Console.WriteLine(s1.ToString());//Student { Id = 2, Name = s1 }，不会因为Name写在外面所以ToString()就不展现了

Student s2 = new Student(3);//第二种写法
s2.Name = "s2";
Console.WriteLine(s2.ToString());//Student { Id = 3, Name = s2 }，不会因为Name写在外面所以ToString()就不展现了


//自定义构造函数
Cat c1 = new Cat(1, "猫", 20);
Console.WriteLine(c1.ToString());//Cat { Id = 1, Name = 猫, Age = 20 }



//浅拷贝，record本质是普通类，所以赋值也是引用赋值，但是可以通过with来浅赋值，这样就不是同一对象
Cat c2 = c1 with { }; //创建副本，只是内容完全一样
Console.WriteLine(c2.ToString());//Cat { Id = 1, Name = 猫, Age = 20 }
Console.WriteLine(c2==c1);//t
Console.WriteLine(Object.ReferenceEquals(c1,c2));//f
//还可以浅赋值某些字段（可以是只读字段）
Cat c3 = c1 with { Id=1888 };
Console.WriteLine(c3.ToString());//Cat { Id = 1888, Name = 猫, Age = 20 }
 
*/
#endregion