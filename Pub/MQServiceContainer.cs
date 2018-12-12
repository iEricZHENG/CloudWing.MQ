using CloudWing.MQ.Core;
using CloudWing.MQ.RabbitMQ;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pub
{
    public class MQServiceContainer : IMQServiceContainer
    {
        readonly IRabbitMQClient rabbitMQClient;
        public MQServiceContainer(IRabbitMQClient rabbitMQClient)
        {
            this.rabbitMQClient = rabbitMQClient;
            PublisherDictionary = new Dictionary<Type, RabbitPublisher>
            {
                { typeof(MyPub),new RabbitPublisher("Demo", "demo", 4)}
            };
        }
        public Dictionary<Type, RabbitPublisher> PublisherDictionary { get; }

        public ConcurrentDictionary<Type, IMQService> ServiceDictionary = new ConcurrentDictionary<Type, IMQService>();
        public async ValueTask<IMQService> GetService<T>(T TPub)
        {
            var type = TPub.GetType();
            if (!ServiceDictionary.TryGetValue(type, out var service))
            {
                if (PublisherDictionary.TryGetValue(type, out var value))
                {
                    var buildTask = value.Build(rabbitMQClient);
                    if (!buildTask.IsCompleted)
                        await buildTask;
                    service = ServiceDictionary.GetOrAdd(type, key => new RabbitMQService(value));
                }
                else
                {
                    throw new NotImplementedException(nameof(GetService));
                }
            }
            return service;
        }
    }
}
