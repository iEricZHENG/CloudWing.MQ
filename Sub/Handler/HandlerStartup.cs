using CloudWing.MQ.Core;
using CloudWing.MQ.RabbitMQ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sub.Handler
{
    public class HandlerStartup
    {
        readonly ISubManager subManager;
        public HandlerStartup(ISubManager subManager)
        {
            this.subManager = subManager;
        }
        public Dictionary<SubscriberGroup, List<Subscriber>> Subscribers => new Dictionary<SubscriberGroup, List<Subscriber>>
        {
            {
                SubscriberGroup.Core,new List<Subscriber>
                {
                    new RabbitSubscriber(typeof(MySubHandler) ,"Demo", "demo",  4)
                }
            }
        };
        public Task Start(SubscriberGroup group, string node = null, List<string> nodeList = null)
        {
            return subManager.Start(Subscribers[group], group.ToString(), node, nodeList);
        }
    }
    public enum SubscriberGroup
    {
        Core = 1,
        Rep = 2,
        Db = 3
    }
}
