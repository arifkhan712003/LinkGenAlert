using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlertGeneratorService
{
    class AlertThreshold : IAlertThreshold
    {
        public int SubscriberId { get; set; }

        public int AllowedVolume { get; set; }

        public int AllowedNoOfFiles { get; set; }
    }
}
