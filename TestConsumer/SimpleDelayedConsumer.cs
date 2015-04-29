using System;
using RabbitMQ.Client;

namespace TestConsumer
{
    public class SimpleDelayedConsumer : ConsumerBase
    {
        protected override string Info
        {
            get { return "Consuming (noack) from delayed queue"; }
        }

        protected override bool Noack
        {
            get { return true; }
        }

        protected override string QueueName
        {
            get { return Names.DelayedQueueName; }
        }
    }
}