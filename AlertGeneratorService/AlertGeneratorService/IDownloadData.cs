﻿namespace AlertGeneratorService
{
    public interface IDownloadData
    {
        string FileName { get; set; }

        string IpAddress { get; set; }

        int FileSizeInBytes { get; set; }

        int SubscriberId { get; set; }
    }
}