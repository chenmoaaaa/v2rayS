using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace v2rayS.Utils
{
    class HttpServer
    {
        public static HttpListener Listener = new HttpListener();

        public static void StartListener()
        {
            Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
            Listener.Prefixes.Add("http://127.0.0.1:1081/pac/");

            Listener.Start();
            new Task(() =>
            {
                while (true)
                {
                    //等待请求连接
                    //没有请求则GetContext处于阻塞状态
                    try
                    {
                        HttpListenerContext ctx = Listener.GetContext();
                        ctx.Response.StatusCode = 200;//设置返回给客服端http状态代码
                        string name = ctx.Request.QueryString["name"];

                        if (name != null)
                        {
                            Console.WriteLine(name);
                        }


                        //使用Writer输出http响应代码
                        using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                        {
                            ctx.Response.ContentType = "application/x-ns-proxy-autoconfig";
                            writer.WriteLine(File.ReadAllText(Directory.GetCurrentDirectory() + "\\pac"));
                            writer.Close();
                            ctx.Response.Close();
                        }
                    }
                    catch (Exception) {}
                }
            }).Start();
        }
    }
}
