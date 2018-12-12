using System;
using System.Threading.Tasks;
using CloudWing.MQ.Core;
using ProtoBuf;
using Ray.Core.Utils;

namespace CloudWing.MQ.RabbitMQ
{
    public class RabbitMQService : IMQService
    {
        readonly RabbitPublisher publisher;
        public RabbitMQService(RabbitPublisher publisher) => this.publisher = publisher;

        public async ValueTask Publish(byte[] bytes, string hashKey)
        {
            var task = publisher.GetQueue(hashKey);
            if (!task.IsCompleted)
                await task;
            var (queue, model) = task.Result;
            model.Publish(bytes, publisher.Exchange, queue, false);
        }
        //public async ValueTask PublishMsg(IMessage msg, string hashKey)
        //{
        //    if (string.IsNullOrEmpty(hashKey))
        //        hashKey = Guid.NewGuid().ToString();
        //    using (var ms = new PooledMemoryStream())
        //    {
        //        Serializer.Serialize(ms, msg);
        //        var data = new W
        //        {
        //            TypeCode = msg.GetType().FullName,
        //            BinaryBytes = ms.ToArray()
        //        };
        //        ms.Position = 0;
        //        ms.SetLength(0);
        //        Serializer.Serialize(ms, data);
        //        var task = Publish(ms.ToArray(), hashKey);
        //        if (!task.IsCompleted) await task;
        //    }
        //}
    }
}
