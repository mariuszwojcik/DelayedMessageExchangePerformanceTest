using System;

namespace TestPublisher
{
    public struct StatRecord
    {
        public DateTime Timestamp { get; set; }
        public long Count { get; set; }

        public StatRecord(long count) : this()
        {
            Timestamp = DateTime.UtcNow;
            Count = count;
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", Timestamp, Count);
        }
    }
}