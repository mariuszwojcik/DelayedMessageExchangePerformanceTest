namespace TestPublisher
{
    public class Test1 : TestBase
    {
        private const string TestDescription = @"Test1 - publish 1M message to regualar exchange.";

        public void Run()
        {
            Logger.Info(TestDescription);

            var regularMessagePublisher = new RegularMessagePublisher(Connection);
            regularMessagePublisher.Run();
        }
    }
}