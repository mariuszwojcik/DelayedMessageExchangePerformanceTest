using System.Collections.Generic;
using RabbitMQ.Client;

namespace TestPublisher
{
    public class RegularMessagePublisher : MessagePublisherBase
    {
        public RegularMessagePublisher(IConnection connection)
            : base(connection)
        { }

        protected override string ExchangeName
        {
            get { return Names.RegularExchangeName; }
        }

        protected override string MessagePostfix
        {
            get { return ""; }
        }

        protected override Dictionary<string, object> MessageHeaders
        {
            get { return new Dictionary<string, object> { }; }
        }
    }
}