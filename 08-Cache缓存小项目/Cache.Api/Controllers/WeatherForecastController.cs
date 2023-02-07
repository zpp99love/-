using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
namespace Cache.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMemoryCache _memoryCache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    [ResponseCache(Duration=60)]
    [HttpGet("Now")]
    public DateTime Now()
    {
        return DateTime.Now;
    }

    [HttpGet("A")]
    public DateTime A()
    {
        
    }



    public record Book(int Id,string Name);
    [HttpGet("GetBook/{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
       //模拟数据库
        var bookList = new List<Book>{new Book(1,"意林"),new Book(2,"非暴力沟通")};
 
        Console.WriteLine("进入缓存查"); 
        Book? b = await _memoryCache.GetOrCreateAsync("Book"+id,async(e)=>{
            Console.WriteLine("到数据库中查");
            //设置绝对时间
            e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.Next(10,15));
            //设置随机时间
            e.SlidingExpiration = TimeSpan.FromSeconds(3);
            return  bookList.Where(x=>x.Id==id).FirstOrDefault();
        });
        
        if(b==null)
        {
            return NotFound($"找不到id为{id}的书");
        }
        Console.WriteLine($"查到了名称为{b.Name}"); 
        return b;
    }


    [HttpGet("GetBook1/{id}")]
    public async Task<ActionResult<Book?>> GetBook1(int id)
    {
       //模拟数据库
        var bookList = new List<Book>{new Book(1,"意林"),new Book(2,"非暴力沟通")};

        string cachekey = "Book"+id;

        System.Console.WriteLine($"正在缓存中查询");
        Book? b = await _memoryCache.GetOrCreateAsync(cachekey,async(e) => {
            System.Console.WriteLine($"缓存没有数据，正在查询数据库并写入缓存");
            return  bookList.Where(x=>x.Id==id).FirstOrDefault();
        });
        
        return b;
    }




    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }



    [HttpGet("GetException")]
    public dynamic GetException()
    {
        string n = null;
        
        return n.ToString();
    }



    

}
