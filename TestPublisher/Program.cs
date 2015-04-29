using System;

namespace TestPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            switch (OperationSelector())
            {
                case 1:
                    new Test1().Run();
                    break;
                case 2:
                    new Test2().Run();
                    break;
                case 3:
                    new Test3().Run();
                    break;
                case 4:
                    new Test4().Run();
                    break;
            }
        }

        private static int OperationSelector()
        {
            int result;
            do
            {
                Console.WriteLine("Choose operation to run:");
                Console.WriteLine("1. publish 1M messages to regular exchange");
                Console.WriteLine("2. publish 1M messages to delayed exchange with no delay");
                Console.WriteLine("3. publish 1M messages to delayed exchange with 1 sec delay");
                Console.WriteLine("4. publish 1M messages to delayed exchange with 2 min delay");
                Console.Write("> ");
                var i = Console.ReadLine();
                if (!Int32.TryParse(i, out result))
                    result = 0;
            } while (result < 1 || result > 4);

            return result;
        }
    }
}
