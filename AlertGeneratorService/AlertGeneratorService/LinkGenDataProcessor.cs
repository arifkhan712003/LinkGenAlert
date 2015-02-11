using System;
using System.Collections.Generic;

namespace AlertGeneratorService
{
    public class LinkGenDataProcessor
    {
        public Dictionary<int, LinkGenProcessedData> GetProcessedData(DateTime startTime, int durationInSeconds)
        {
            var processedLinkGenDatas = new Dictionary<int, LinkGenProcessedData>();
            
            var rawLinkGenDatas = GetLastUsage(startTime, durationInSeconds);

            foreach (var rawLinkGenData in rawLinkGenDatas)
            {
                int tenentId = rawLinkGenData.TenentId;

                if (processedLinkGenDatas.ContainsKey(tenentId))
                {
                    LinkGenProcessedData linkGenProcessedData = processedLinkGenDatas[tenentId];
                    linkGenProcessedData.FileCount += 1;
                    linkGenProcessedData.FileSizeInBytes += rawLinkGenData.FileSizeInBytes;
                }
                else
                {
                    var linkGenProcessedData = new LinkGenProcessedData
                    {
                        TenentId = rawLinkGenData.TenentId,
                        FileCount = 1,
                        FileSizeInBytes = rawLinkGenData.FileSizeInBytes
                    };

                    processedLinkGenDatas.Add(rawLinkGenData.TenentId,linkGenProcessedData);
                }
            }

            return processedLinkGenDatas;
        }

        private List<IDownloadData> GetLastUsage(DateTime startTime, int durationInSeconds)
        {
            //iterate through some list fetched and insert into the list
            return new List<IDownloadData>();
        }
    }
}