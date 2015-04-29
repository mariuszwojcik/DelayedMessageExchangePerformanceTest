using System;

namespace TestPublisher
{
    public class Test2 : TestBase
    {
        private const string TestDescription = @"Test2 - publish 1M messages to delayed exchange with delay set to 0.";
        private const int MessageToPublish = 1000000;

        public void Run()
        {
            Logger.Info(TestDescription);

            var delayedMessagePublisher = new DelayedMessagePublisher(Connection, TimeSpan.Zero);
            delayedMessagePublisher.Run(MessageToPublish);

            Logger.InfoFormat("Delayed message publisher stats:\r\nPublished {0} messages in {1}, {2}m/s\r\n{3}"
                , MessageToPublish
                , delayedMessagePublisher.Watch.Elapsed
                , MessageToPublish / delayedMessagePublisher.Watch.Elapsed.TotalSeconds
                , String.Join("\r\n", delayedMessagePublisher.Stats));
        }
    }
}