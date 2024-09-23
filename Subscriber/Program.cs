using System.Threading.Tasks;
using Lib;

namespace Subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var task = Start();

            task.GetAwaiter().GetResult();
        }

        public static async Task Start()
        {
            Bus.BusConfig = new BusConfig
            {
                MessageBrokerType = MessageBrokerType.RabbitMQ,
                Host = "localhost",
                VirtualHost = "/",
                Username = "guest",
                Password = "guest"
            };

            await Bus.Subscribe();

            await Task.Delay(50000);

            await Bus.StopBus();
        }
    }
}
