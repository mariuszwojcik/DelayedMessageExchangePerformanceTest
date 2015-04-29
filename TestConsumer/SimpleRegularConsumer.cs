using System;
using RabbitMQ.Client;

namespace TestConsumer
{
    public class SimpleRegularConsumer : ConsumerBase
    {
        protected override string Info
        {
            get { return "Consuming (noack) from regular queue"; }
        }

        protected override bool Noack
        {
            get { return true; }
        }

        protected override string QueueName
        {
            get { return Names.RegularQueueName; }
        }
    }
}