using System;
using System.Threading.Tasks;
using MassTransit;

namespace Lib
{
    public class MessageConsumer : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine(context.Message.Value);

            return Task.FromResult(0);
        }
    }
}