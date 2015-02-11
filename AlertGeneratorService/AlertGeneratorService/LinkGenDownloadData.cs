using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlertGeneratorService
{
    class LinkGenDownloadData : IDownloadData
    {
        public int TenentId { get; set; }

        public string FileName { get; set; }

        public string IpAddress { get; set; }

        public int FileSizeInBytes { get; set; }
    }
}
