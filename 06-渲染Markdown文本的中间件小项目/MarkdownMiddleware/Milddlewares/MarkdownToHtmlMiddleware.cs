using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarkdownMiddleware.Milddlewares
{
    public class MarkdownToHtmlMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment hostEn;

        public MarkdownToHtmlMiddleware(RequestDelegate next, IWebHostEnvironment hostEn)
        {
            this.next = next;
            this.hostEn = hostEn;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.ToString();
            var file = hostEn.WebRootFileProvider.GetFileInfo(path);
            if (!file.Exists)
            {
                //不存在则交给其他中间件处理
                await next.Invoke(context);
                //就不走该中间件的后逻辑了
                return;
            }
            //是md文件才处理
            if (!path.EndsWith(".md",true,null))
            {
                await next.Invoke(context);
                return;
            }

            //把Markdown流变成Markdown字符串：
                using var stream = file.CreateReadStream();
                //安装Ude.NetStandard包，推断文件编码格式
                Ude.CharsetDetector cdet = new Ude.CharsetDetector();
                cdet.Feed(stream);
                cdet.DataEnd();
                //如果推断不出则默认为UTF-8
                string charset = cdet.Charset ?? "UTF-8";
                //之前推断的时候读了，现在复位
                stream.Position = 0;
                using StreamReader reader = new StreamReader(stream, Encoding.GetEncoding(charset));
                string mdText = await reader.ReadToEndAsync();


            //Markdown字符串转化html：
                //安装MarkdownSharp包，将md文本转化为html
                MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();
                string html = md.Transform(mdText);
                context.Response.ContentType = "Text/html;charset=UTF-8";
                await context.Response.WriteAsync(html);



        }
    }
}
