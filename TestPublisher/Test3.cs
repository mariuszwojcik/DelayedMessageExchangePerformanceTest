namespace TestPublisher
{
    public class Test3 : TestBase
    {
        private const string TestDescription = @"Test3 - publish 1M messages to delayed exchange with delay set to 1 second.";

        public void Run()
        {
            Logger.Info(TestDescription);

            var delayedMessagePublisher = new DelayedMessagePublisher(Connection, 1000);
            delayedMessagePublisher.Run();
        }
    }
}