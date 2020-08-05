using System;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace CaptiveDependencies
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddSingleton<SingletonThing>();
            services.AddTransient<TransientThing>();

            var provider = services.BuildServiceProvider();

            //var provider = services.BuildServiceProvider();

            var sinlgeton1 = provider.GetService<SingletonThing>();
            var sinlgeton2 = provider.GetService<SingletonThing>();
            
            sinlgeton1.CallTransient();
            sinlgeton2.CallTransient();
        }
    }

    public class TransientThing
    {
        public void CallMe() => Console.WriteLine($"TRANSIENT HASHCODE: {GetHashCode()}");
    }

    public class SingletonThing
    {
        private readonly TransientThing _transientThing;

        public SingletonThing(TransientThing transientThing)
        {
            _transientThing = transientThing;
        }

        public void CallTransient()
        {
            Console.WriteLine($"SINGLETON HASHCODE: {GetHashCode()}");
            _transientThing.CallMe();
        }
    }
}