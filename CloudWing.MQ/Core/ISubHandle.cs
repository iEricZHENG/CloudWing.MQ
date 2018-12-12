using System.Threading.Tasks;

namespace CloudWing.MQ.Core
{
    public interface ISubHandler
    {
        Task Notice(byte[] data);
    }
}
