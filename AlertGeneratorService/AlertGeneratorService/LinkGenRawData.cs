using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlertGeneratorService
{
    class LinkGenRawData : IDownloadData
    {
        public int SubscriberId { get; set; }

        public string FileName { get; set; }

        public string IpAddress { get; set; }

        public int FileSizeInBytes { get; set; }
    }
}
