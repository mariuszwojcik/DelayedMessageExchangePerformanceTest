using System;
using System.Collections.Generic;
using System.Configuration;
using log4net;
using RabbitMQ.Client;

namespace TestPublisher
{
    public abstract class TestBase : IDisposable
    {
        protected const string RegularExchangeName = "RegularExchange";
        protected const string DelayedExchangeName = "DelayedExchange";
        protected const string RegularQueueName = "RegularQueue";
        protected const string DelayedQueueName = "DelayedQueue";
        protected const string RoutingKey = "test";

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILog _logger;

        protected TestBase()
        {
            var factory = new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["RabbitMqHost"], 
                VirtualHost = "/",
                UserName = ConfigurationManager.AppSettings["RabbitMqUsername"],
                Password = ConfigurationManager.AppSettings["RabbitMqPassword"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = LogManager.GetLogger("");

            DeclareRegularExchange();
            DeclareDelayedExchange();
            DeclareRegularQueue();
            DeclareDelayedQueue();
        }

        protected IModel Channel
        {
            get { return _channel; }
        }

        protected ILog Logger
        {
            get { return _logger; }
        }

        public IConnection Connection
        {
            get { return _connection; }
        }

        protected void DeclareRegularExchange()
        {
            _channel.ExchangeDeclare(RegularExchangeName, "direct", true, false, null);
        }

        protected void DeclareDelayedExchange()
        {
            IDictionary<string, object> args = new Dictionary<string, object>
            {
                {"x-delayed-type", "direct"}
            };
            _channel.ExchangeDeclare(DelayedExchangeName, "x-delayed-message", true, false, args);
        }

        protected void DeclareRegularQueue()
        {
            var queue = Channel.QueueDeclare(RegularQueueName, true, false, false, null);
            Channel.QueueBind(queue, RegularExchangeName, RoutingKey);
        }

        protected void DeclareDelayedQueue()
        {
            var queue = Channel.QueueDeclare(DelayedQueueName, true, false, false, null);
            Channel.QueueBind(queue, DelayedExchangeName, RoutingKey);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_channel != null) _channel.Dispose();
                if (_connection != null) _connection.Dispose();
            }
        }
    }
}