using System;

namespace ScopedInjectionExample
{
    public class ReceiverContext : IDisposable
    {
        public ReceiverContext()
        {
            Console.WriteLine($"   ReceiverContext Created {CurrentEmployeeId}: {GetHashCode()}");
        }
            
        public string CurrentEmployeeId { get; set; }

        public void Dispose()
        {
            Console.WriteLine($"   ReceiverContext Disposed: {CurrentEmployeeId}: {GetHashCode()}");
        }
    }
}