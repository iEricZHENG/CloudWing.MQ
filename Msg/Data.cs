using System;
using CloudWing.MQ.Core;
using ProtoBuf;

namespace Msg
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Data : IMessage
    {
        public Data() { }
        public String Name { get; set; } = "小明";
    }
}
