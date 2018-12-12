using System.Threading.Tasks;

namespace CloudWing.MQ.Core
{
    public interface IMQServiceContainer
    {
        ValueTask<IMQService> GetService<T>(T TPub);
    }
}
