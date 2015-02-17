using System;

namespace AlertGeneratorService
{
    public class AttributeData
    {
        public int SubscriberId { get; set; }

        public int AttributeId { get; set; }

        public int AttributeValue { get; set; }

        public DateTime StartDate { get; set; }
    }
}