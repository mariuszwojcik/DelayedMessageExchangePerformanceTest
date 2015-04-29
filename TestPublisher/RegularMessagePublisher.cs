using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace TestPublisher
{
    public class RegularMessagePublisher
    {
        private readonly IModel _model;
        private readonly IList<StatRecord> _stats;
        private readonly Stopwatch _watch;
        private long _counter;

        public RegularMessagePublisher(IConnection connection)
        {
            _model = connection.CreateModel();
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
            for (var i = 0; i < (int)noOfMessagesToPublish; i++)
            {
                var props = _model.CreateBasicProperties();
                props.Headers = new Dictionary<string, object> { };
                _model.BasicPublish(Names.RegularExchangeName, Names.RoutingKey, props, Encoding.Default.GetBytes("Hello World!"));

                Interlocked.Increment(ref _counter);
            }
        }
    }
}