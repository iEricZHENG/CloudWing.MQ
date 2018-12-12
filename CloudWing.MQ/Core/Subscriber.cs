using System;

namespace CloudWing.MQ.Core
{
    public class Subscriber
    {
        public Subscriber(Type handler)
        {
            Handler = handler;
        }
        public Type Handler { get; }
    }
}
