using System;
using System.Reflection;
using CloudWing.MQ.Core;
using CloudWing.MQ.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Sub.Handler;

namespace Sub
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var servicecollection = new ServiceCollection();
            servicecollection.AddSingleton<ISerializer, ProtobufSerializer>();//注册序列化组件
            servicecollection.AddRabbitMQ();//注册RabbitMq为默认消息队列
            servicecollection.AddLogging(logging => logging.AddConsole());
            servicecollection.AddMQHandler();
            servicecollection.PostConfigure<RabbitConfig>(c =>
            {
                c.UserName = "admin";
                c.Password = "admin";
                c.Hosts = new[] { "127.0.0.1:5672" };
                c.MaxPoolSize = 100;
                c.VirtualHost = "/";
            });
            var provider = servicecollection.BuildServiceProvider();
            
            Assembly.LoadFile($"{AppDomain.CurrentDomain.BaseDirectory}Msg.dll");
            var handlerStartup = provider.GetService<HandlerStartup>();
            await handlerStartup.Start(SubscriberGroup.Core);
        }
    }
}
