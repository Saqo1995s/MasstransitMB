using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Configuration;
using RabbitMQ.Client.Exceptions;
using static MassTransit.MessageHeaders;

namespace Lib
{
    public static class Bus
    {
        private static IBusControl BusControl { get; set; }

        public static BusConfig BusConfig { get; set; }

        /// <summary>
        /// Configuring and starting message broker bus
        /// </summary>
        /// <param name="type">Type of the message</param>
        /// <param name="busConfig">Object with user data</param>
        /// <exception cref="UnsupportedMethodException"></exception>
        private static async Task StartBus()
        {
            switch (BusConfig.MessageBrokerType)
            {
                case MessageBrokerType.RabbitMQ:
                    BusControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(BusConfig.Host, BusConfig.VirtualHost, h =>
                        {
                            h.Username(BusConfig.Username);
                            h.Password(BusConfig.Password);
                        });
                    });
                        
                    break;
                case MessageBrokerType.ServiceBus:
                    BusControl = MassTransit.Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {
                        cfg.Host(BusConfig.ConnectionString); //Connection string
                    });
                    break;
                case MessageBrokerType.Kafka:
                    throw new UnsupportedMethodException("Kafka currently not supported!");
            }

            await BusControl.StartAsync();
        }

        public static async Task StopBus()
        {
            await BusControl.StopAsync();
        }

        public static async Task SendAsync<T>(string queue, T message) where T : class
        {
            ISendEndpoint endpoint = await BusControl.GetSendEndpoint(new Uri($"queue:{queue}"));
            await endpoint.Send(message);
        }

        /// <summary>
        /// Use to publish messages to the queue
        /// </summary>
        /// <typeparam name="T">Type of the message</typeparam>
        /// <param name="queue">Queue name</param>
        /// <param name="message">Message to post</param>
        /// <returns></returns>
        public static async Task Publish<T>(string queue, Message message) where T : class
        {
            await StartBus();
            await SendAsync(queue, message);
            await StopBus();
        }

        /// <summary>
        /// Subscribe to the endpoint and wait for consume
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnsupportedMethodException"></exception>
        public static async Task Subscribe()
        {
            switch (BusConfig.MessageBrokerType)
            {
                case MessageBrokerType.RabbitMQ:
                    BusControl = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(BusConfig.Host, BusConfig.VirtualHost, h =>
                        {
                            h.Username(BusConfig.Username);
                            h.Password(BusConfig.Username);
                        });

                        cfg.ReceiveEndpoint("Automate", ec =>
                        {
                            // for example, MessageConsumer consumes IMessages
                            ec.Consumer<MessageConsumer>();
                        });

                    });

                    break;
                case MessageBrokerType.ServiceBus:
                    BusControl = MassTransit.Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {
                        cfg.Host(BusConfig.ConnectionString); //Connection string

                        cfg.ReceiveEndpoint("queue1", ec1 =>
                        {
                            ec1.Consumer<MessageConsumer>();
                        });
                    });
                    break;
                case MessageBrokerType.Kafka:
                    throw new UnsupportedMethodException("Kafka currently not supported!");
            }

            await BusControl.StartAsync();
        }
    }
}
