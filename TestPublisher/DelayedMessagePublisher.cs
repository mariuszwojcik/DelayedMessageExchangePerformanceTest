using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace TestPublisher
{
    public class DelayedMessagePublisher : MessagePublisherBase
    {
        private readonly int _delay;

        public DelayedMessagePublisher(IConnection connection, int delay) : base(connection)
        {
            _delay = delay;
        }

        protected override string ExchangeName
        {
            get { return Names.DelayedExchangeName; }
        }

        protected override string MessagePostfix
        {
            get { return String.Format("(delay: {0})", _delay); }
        }

        protected override Dictionary<string, object> MessageHeaders
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {"x-delay", _delay}
                }; ;
            }
        }
    }
}