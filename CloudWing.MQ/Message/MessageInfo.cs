using ProtoBuf;

namespace CloudWing.MQ.Core
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class MessageInfo: IMessageWrapper
    {
        public string TypeCode { get; set; }
        public byte[] BinaryBytes { get; set; }
    }
}
