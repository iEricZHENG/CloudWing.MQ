using System.Threading.Tasks;

namespace CloudWing.MQ.Core
{
    public interface IMQService
    {
        ValueTask Publish(byte[] bytes, string hashKey);
    }
}
