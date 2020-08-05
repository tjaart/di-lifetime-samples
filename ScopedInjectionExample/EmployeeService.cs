using System;

namespace ScopedInjectionExample
{
    public class EmployeeService : IDisposable
    {
        private readonly ReceiverContext _receiverContext;

        public EmployeeService(ReceiverContext receiverContext)
        {
            _receiverContext = receiverContext;
            Console.WriteLine($"      EmployeeService Created {WhoAmI}: {GetHashCode()}");
        }

        public string WhoAmI => $"{_receiverContext.CurrentEmployeeId}" ;

        public void Dispose()
        {
            Console.WriteLine($"      EmployeeService Disposed {WhoAmI}: {GetHashCode()}");
        }
    }
}