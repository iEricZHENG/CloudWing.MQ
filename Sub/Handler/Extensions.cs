using Microsoft.Extensions.DependencyInjection;

namespace Sub.Handler
{
    public static class Extensions
    {
        public static void AddMQHandler(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<HandlerStartup>();
            serviceCollection.AddSingleton<MySubHandler>();
        }
    }
}
