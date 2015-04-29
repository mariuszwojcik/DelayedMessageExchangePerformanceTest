using System;
using System.Threading;
using RabbitMQ.Client;

namespace TestConsumer
{
    public abstract class ConsumerBase
    {
        private readonly Timer _statsTimer;
        private long _counter;

        protected ConsumerBase()
        {
            _statsTimer = new Timer(StatsTimerProc, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void StatsTimerProc(object state)
        {
            var c = Interlocked.Exchange(ref _counter, 0);
            Console.WriteLine("consuming {0} msgs/sec", c);
        }

        protected abstract string Info { get; }

        protected abstract bool Noack { get; }

        protected abstract string QueueName { get; }

        public void Consume()
        {
            Console.WriteLine(Info);

            var factory = new ConnectionFactory { HostName = "localhost", VirtualHost = "/" };
            var connection = factory.CreateConnection();
            var model = connection.CreateModel();

            model.BasicQos(0, 50000, false);
            var consumer = new QueueingBasicConsumer(model);
            model.BasicConsume(QueueName, Noack, consumer);
            do
            {
                var args = consumer.Queue.Dequeue();
                if (!Noack)
                    model.BasicAck(args.DeliveryTag, false);
            } while (true);
        }
    }
}