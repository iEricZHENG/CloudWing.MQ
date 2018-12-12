using System.Threading.Tasks;

namespace CloudWing.MQ.RabbitMQ
{
    public interface IRabbitMQClient : System.IDisposable
    {
        Task ExchangeDeclare(string exchange);
        void PushModel(ModelWrapper model);
        ValueTask<ModelWrapper> PullModel();
    }
}
