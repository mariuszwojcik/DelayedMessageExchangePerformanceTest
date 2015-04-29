namespace TestPublisher
{
    public class Test4 : TestBase
    {
        private const string TestDescription = @"Test4 - publish 1M messages to delayed exchange with delay set to 1 minute.";

        public void Run()
        {
            Logger.Info(TestDescription);

            var delayedMessagePublisher = new DelayedMessagePublisher(Connection, 60000);
            delayedMessagePublisher.Run();
        }
    }
}