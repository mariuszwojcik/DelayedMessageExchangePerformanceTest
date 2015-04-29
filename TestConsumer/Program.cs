using System;

namespace TestConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (OperationSelector())
            {
                case 1:
                    new SimpleRegularConsumer().Consume();
                    break;
                case 2:
                    new AckingRegularConsumer().Consume();
                    break;
                case 3:
                    new SimpleDelayedConsumer().Consume();
                    break;
                case 4:
                    new AckingDelayedConsumer().Consume();
                    break;
            }
        }

        private static int OperationSelector()
        {
            int result;
            do
            {
                Console.WriteLine("Choose operation to run:");
                Console.WriteLine("1. regular queue consumer NOACK");
                Console.WriteLine("2. regular queue consumer ACK");
                Console.WriteLine("3. delayed queue consumer NOACK");
                Console.WriteLine("4. delayed queue consumer ACK");
                Console.Write("> ");
                var i = Console.ReadLine();
                if (!Int32.TryParse(i, out result))
                    result = 0;
            } while (result < 1 || result > 4);

            return result;
        }
    }
}
