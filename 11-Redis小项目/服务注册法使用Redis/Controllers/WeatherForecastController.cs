using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
//using Zack.ASPNETCore;

namespace 服务注册法使用Redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        //普通Redis
        private readonly IDistributedCache _distributedCache; 

        private readonly List<Book> books= new List<Book>() 
        {
            new Book(1,"书1"),
            new Book(2,"书2"),
            new Book(3,"书3"),
            new Book(4,"书4"),
            new Book(5,"书5"),
            new Book(6,"书6"),
            new Book(7,"书7")
        };

        public record Book(int Id,string Name);

        //Redis缓存帮助类
        private readonly IDistributedCacheHelper _distributedCacheHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache distributedCache, IDistributedCacheHelper distributedCacheHelper)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _distributedCacheHelper = distributedCacheHelper;
        }


        /// <summary>
        /// 普通缓存
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test1/id")]
        public async Task<ActionResult<Book?>> test1(int id)
        {
            Book? book;
            string? b = await _distributedCache.GetStringAsync("book" + id);//有但值为null则返回"null",没有就返回null，为了防止缓存穿透
            if (b == null)
            {
                Console.WriteLine("从数据库中取");
                book = books.Where(x => x.Id == id).SingleOrDefault();
                //if(book!=null)//不能加这个，否则会导致缓存穿透
                //只能存byte或string类型
                    await _distributedCache.SetStringAsync("book" + id, JsonSerializer.Serialize(book));//null会被Serialize序列化为字符串"null",防止缓存穿透
            }
            else
            {
                Console.WriteLine("从Redis缓存中取");
                book = JsonSerializer.Deserialize<Book>(b);
            }
            if (book == null)
            {
                return NotFound("不存在");
            }
            else
            { 
                return book;
            }
            
        }


        
        /// <summary>
        /// 缓存帮助类（好像是存到杨的Redis中去了！）我把两个类复制到本地就解决了，还真是存到他哪，也说明可能他的Program也打包到nuget里了！
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("test2/id")]
        public async Task<ActionResult<Book?>> test2(int id)
        {
            Console.WriteLine("从Redis缓存中取");
            var book = await _distributedCacheHelper.GetOrCreateAsync("book" + id,async (e) => 
            {
                Console.WriteLine("缓存没有，从数据库中取");
                e.SlidingExpiration = TimeSpan.FromSeconds(5);//滑动过期时间
                var list_book =  books.Where(x => x.Id == id).SingleOrDefault();
                return list_book;
            },20);//绝对过期时间（并且源码封装了这个绝对过期时间是设置的值范围的随机数，防止缓存雪崩）

            
            if (book == null)
            {
                return NotFound("不存在");
            }
            else
            {
                return book;
            }

        }



    }
}