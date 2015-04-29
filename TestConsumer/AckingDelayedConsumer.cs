namespace TestConsumer
{
    public class AckingDelayedConsumer : ConsumerBase
    {
        protected override string Info
        {
            get { return "Consuming (ack) from delayed queue"; }
        }

        protected override bool Noack
        {
            get { return false; }
        }

        protected override string QueueName
        {
            get { return Names.DelayedQueueName; }
        }
    }
}