using System;

namespace TestPublisher
{
    public class Test1 : TestBase
    {
        private const string TestDescription = @"Test1 - publish 1M message to regualar exchange.";
        private const int MessageToPublish = 1000000;

        public void Run()
        {
            Logger.Info(TestDescription);

            var regularMessagePublisher = new RegularMessagePublisher(Connection);
            regularMessagePublisher.Run(MessageToPublish);

            Logger.InfoFormat("Regular message publisher stats:\r\nPublished {0} messages in {1}, {2}m/s\r\n{3}"
                , MessageToPublish
                , regularMessagePublisher.Watch.Elapsed
                , MessageToPublish / regularMessagePublisher.Watch.Elapsed.TotalSeconds
                , String.Join("\r\n", regularMessagePublisher.Stats));
        }
    }
}