using CloudWing.MQ.Core;
using Msg;
using System;
using System.IO;
using System.Threading.Tasks;
using ProtoBuf;

namespace Sub.Handler
{
    public class MySubHandler : SubHandler<MessageInfo>
    {
        public MySubHandler(IServiceProvider svProvider) : base(svProvider) { }
        public override Task Tell(byte[] wrapBytes, byte[] dataBytes, IMessage data, MessageInfo msg)
        {
            using (var wms = new MemoryStream(wrapBytes))
            {
                var message = Serializer.Deserialize<MessageInfo>(wms);
                using (var ems = new MemoryStream(message.BinaryBytes))
                {
                    var dt = Serializer.Deserialize<Data>(ems);
                    Console.WriteLine($"{dt.Name}");
                }
            }
            return Task.CompletedTask;
        }
    }
}
