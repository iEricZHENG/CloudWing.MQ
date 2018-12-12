using Microsoft.Extensions.DependencyInjection;
using CloudWing.MQ.Core;

namespace CloudWing.MQ.RabbitMQ
{
    public static class Extensions
    {
        public static void AddRabbitMQ(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRabbitMQClient, RabbitMQClient>();
            serviceCollection.AddSingleton<ISubManager, RabbitSubManager>();
        }
    }
}
