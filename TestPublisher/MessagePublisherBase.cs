using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using log4net;
using RabbitMQ.Client;

namespace TestPublisher
{
    public abstract class MessagePublisherBase
    {
        private readonly IConnection _connection;
        private readonly Timer _statsCollector;
        private readonly ILog _logger;
        private readonly Stopwatch _watch;
        private long _counter;

        protected MessagePublisherBase(IConnection connection)
        {
            MessagesSize = 100;
            _connection = connection;
            _logger = LogManager.GetLogger("");
            _watch = new Stopwatch();
            _statsCollector = new Timer(StatsTimerProc, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
        }

        private void StatsTimerProc(object state)
        {
            var c = Interlocked.Exchange(ref _counter, 0);
            _logger.InfoFormat("publishing {0} m/sec to {1} {2}. Elapsed: {3}", c, ExchangeName, MessagePostfix, _watch.Elapsed);

        }

        public int MessagesSize { get; set; }

        protected abstract string ExchangeName { get; }

        protected abstract string MessagePostfix { get; }

        protected abstract Dictionary<string, object> MessageHeaders { get; }

        public void Run()
        {
            var message = Encoding.ASCII.GetBytes(new String('*', MessagesSize));
            _watch.Start();
            var model = _connection.CreateModel();
            while(true)
            {
                var props = model.CreateBasicProperties();
                props.Headers = MessageHeaders;
                model.BasicPublish(ExchangeName, Names.RoutingKey, props, message);

                Interlocked.Increment(ref _counter);
            }
        }
    }
}