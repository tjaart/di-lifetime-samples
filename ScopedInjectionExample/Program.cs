using Microsoft.Extensions.DependencyInjection;

namespace ScopedInjectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<RequestListener>();
            serviceCollection.AddScoped<ReceiverContext>();
            serviceCollection.AddTransient<EmployeeService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var simpsonsRequests = serviceProvider.GetService<RequestListener>();

            simpsonsRequests.IncomingRequest("Homer");
            simpsonsRequests.IncomingRequest("Lisa");

        }
    }
}