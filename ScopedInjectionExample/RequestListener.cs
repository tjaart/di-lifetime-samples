using System;
using Microsoft.Extensions.DependencyInjection;

namespace ScopedInjectionExample
{
    public class RequestListener : IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RequestListener(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            Console.WriteLine("RequestListener Created");
        }

        public void IncomingRequest(string employeeId)
        {
            // entry point for clients, such as http clients, messages on a queue, direct tcp etc.
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var receiverContext = scope.ServiceProvider.GetService<ReceiverContext>();
                receiverContext.CurrentEmployeeId = employeeId;
                var empService = scope.ServiceProvider.GetService<EmployeeService>();
                Console.WriteLine($"            EMP: {empService.WhoAmI}");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("Requester Disposed");
        }
    }
}