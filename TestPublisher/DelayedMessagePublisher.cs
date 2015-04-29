using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace TestPublisher
{
    public class DelayedMessagePublisher
    {
        private readonly IModel _model;
        private readonly TimeSpan _delay;
        private readonly IList<StatRecord> _stats;
        private readonly Stopwatch _watch;
        private long _counter;

        public DelayedMessagePublisher(IConnection connection, TimeSpan delay)
        {
            _model = connection.CreateModel();
            _delay = delay;
            _stats = new List<StatRecord>(600);
            _watch = new Stopwatch();
        }

        public IList<StatRecord> Stats
        {
            get { return _stats; }
        }

        public Stopwatch Watch
        {
            get { return _watch; }
        }

        public void Run(int noOfMessagesToPublish)
        {
            var statsCollector = new Timer(state =>
            {
                _stats.Add(new StatRecord(Interlocked.Read(ref _counter)));
            }, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            var publishingThread = new Thread(PublishMessages);
            _watch.Start();
            publishingThread.Start(noOfMessagesToPublish);

            publishingThread.Join();
            _watch.Stop();
            statsCollector.Dispose();
        }

        private void PublishMessages(object noOfMessagesToPublish)
        {
            var delay = (int) _delay.TotalMilliseconds;
            for (var i = 0; i < (int)noOfMessagesToPublish; i++)
            {
                var props = _model.CreateBasicProperties();
                props.Headers = new Dictionary<string, object>
                {
                    {"x-delay", delay}
                };
                _model.BasicPublish(Names.DelayedExchangeName, Names.RoutingKey, props, Encoding.Default.GetBytes("Hello World!"));

                Interlocked.Increment(ref _counter);
            }
        }
    }
}