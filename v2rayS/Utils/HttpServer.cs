using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using v2rayS.Controllers;

namespace v2rayS.Utils
{
    class HttpServer
    {
        public static HttpListener Listener = new HttpListener();
        public static Task CurrentTask;
        public static string PacString = File.ReadAllText(Process.CurrentDirectory + "\\pac");
        private static CancellationTokenSource cts;
        private static CancellationToken ct;
        private static PacWatcher _pacWatcher;

        private static Action action = () =>
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
                        writer.WriteLine(PacString);
                        writer.Close();
                        ctx.Response.Close();
                    }
                }
                catch (Exception) { }
            }
        };

        public static void StartListener()
        {
            //初始化Cancellation
            cts = new CancellationTokenSource();
            ct = cts.Token;

            Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;//指定身份验证 Anonymous匿名访问
            Listener.Prefixes.Add("http://127.0.0.1:1081/pac/");

            Listener.Start();
            CurrentTask = Task.Factory.StartNew(action, ct);

            _pacWatcher = new PacWatcher(Process.CurrentDirectory, "pac");
        }

        public static void Refresh()
        {
            cts.Cancel();
            while(true)
            {
                try
                {
                    PacString = File.ReadAllText(Process.CurrentDirectory + "\\pac");
                    break;
                }
                catch (Exception) { }
            }

            CurrentTask = Task.Factory.StartNew(action, ct);
        }
    }
}
