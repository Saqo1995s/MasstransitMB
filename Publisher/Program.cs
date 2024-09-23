using System;
using System.Threading.Tasks;
using Lib;

namespace Publisher
{
    internal class Program
    {
        public static void Main(string[] args)
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

            try
            {
                do
                {
                    string value = await Task.Run(() =>
                    {
                        Console.WriteLine("Enter message (or quit to exit)");
                        Console.Write("> ");
                        return Console.ReadLine();
                    });

                    if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                        break;

                    await Bus.Publish<Message>("Automate", new Message {Value = value});
                }
                while (true);
            }
            finally
            {
                await Bus.StopBus();
            }
        }
    }
}