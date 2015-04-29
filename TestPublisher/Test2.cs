namespace TestPublisher
{
    public class Test2 : TestBase
    {
        private const string TestDescription = @"Test2 - publish 1M messages to delayed exchange with delay set to 0.";

        public void Run()
        {
            Logger.Info(TestDescription);

            var delayedMessagePublisher = new DelayedMessagePublisher(Connection, 0);
            delayedMessagePublisher.Run();
        }
    }
}