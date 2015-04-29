using System;
using RabbitMQ.Client;

namespace TestConsumer
{
    public class AckingRegularConsumer : ConsumerBase
    {
        protected override string Info
        {
            get { return "Consuming (ack) from regular queue"; }
        }

        protected override bool Noack
        {
            get { return false; }
        }

        protected override string QueueName
        {
            get { return Names.RegularQueueName; }
        }
    }
}