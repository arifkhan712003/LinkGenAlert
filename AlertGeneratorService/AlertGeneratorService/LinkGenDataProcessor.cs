using System;
using System.Collections.Generic;

namespace AlertGeneratorService
{
    public class LinkGenDataProcessor
    {
        public Dictionary<int, AttributeData> GetProcessedData(DateTime startTime, int durationInSeconds)
        {
            var processedLinkGenDatas = new Dictionary<int, AttributeData>();
            
            var rawLinkGenDatas = GetLastUsage(startTime, durationInSeconds);

            foreach (var rawLinkGenData in rawLinkGenDatas)
            {
                if (processedLinkGenDatas.ContainsKey(rawLinkGenData.SubscriberId))
                {
                    AttributeData attributeData = processedLinkGenDatas[rawLinkGenData.SubscriberId];
                    attributeData.AttributeValue += 1;
                }
                else
                {
                    var linkGenProcessedData = new AttributeData
                    {
                        SubscriberId = rawLinkGenData.SubscriberId,
                        AttributeValue = 1,
                    };

                    processedLinkGenDatas.Add(rawLinkGenData.SubscriberId,linkGenProcessedData);
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