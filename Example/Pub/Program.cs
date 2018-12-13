using CloudWing.MQ.Core;
using CloudWing.MQ.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Msg;
using ProtoBuf;
using Ray.Core.Utils;
using System;
using System.Threading.Tasks;

namespace Pub
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var servicecollection = new ServiceCollection();
            servicecollection.AddSingleton<ISerializer, ProtobufSerializer>();//注册序列化组件
            servicecollection.AddRabbitMQ();//注册RabbitMq为默认消息队列
            servicecollection.AddLogging(logging => logging.AddConsole());
            servicecollection.AddRabbitMQ();
            servicecollection.AddSingleton<IMQServiceContainer, MQServiceContainer>();
            servicecollection.PostConfigure<RabbitConfig>(c =>
            {
                c.UserName = "admin";
                c.Password = "admin";
                c.Hosts = new[] { "127.0.0.1:5672" };
                c.MaxPoolSize = 100;
                c.VirtualHost = "/";
            });
            var provider = servicecollection.BuildServiceProvider();
            //
            var serializer = provider.GetService<ISerializer>();
            var mqServiceContainer = provider.GetService<IMQServiceContainer>();
            MyPub pub = new MyPub(mqServiceContainer, serializer);
            await pub.Go();
            Console.WriteLine("Start");
        }
    }

    public class MyPub
    {
        private IMQServiceContainer mQServiceContainer;
        private ISerializer serializer;
        public MyPub(IMQServiceContainer container, ISerializer serializer)
        {
            this.mQServiceContainer = container;
            this.serializer = serializer;
        }

        public async Task Go()
        {
            Data d = new Data();
            var mqService = mQServiceContainer.GetService(this);
            using (var ms = new PooledMemoryStream())
            {
                Serializer.Serialize(ms, d);
                var data = new MessageInfo()
                {
                    TypeCode = d.GetType().FullName,
                    BinaryBytes = ms.ToArray()
                };
                ms.Position = 0;
                ms.SetLength(0);
                Serializer.Serialize(ms, data);
                for (int i = 0; i < 2; i++)
                {
                    await mqService.Result.Publish(ms.ToArray(), Guid.NewGuid().ToString());
                    Console.WriteLine(i);
                }
            }
        }
    }
}
