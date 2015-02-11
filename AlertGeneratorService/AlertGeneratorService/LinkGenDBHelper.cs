using System;

namespace AlertGeneratorService
{
    public class LinkGenDbHelper
    {

        internal void StoreAlertData(System.Collections.Generic.Dictionary<int, LinkGenProcessedData> linkGenProcessedDatas)
        {
            foreach (var linkGenProcessedData in linkGenProcessedDatas)
            {
                StoreAlertData(linkGenProcessedData.Value);
            }
        }

        private void StoreAlertData(LinkGenProcessedData processedData)
        {
            throw new NotImplementedException();
        }

        internal LinkGenProcessedData FetchAlertData(int tenentId, int alertDurationInSeconds)
        {
            throw new NotImplementedException();
        }

        internal IAlertThreshold FetchAlertThreshold(int tenent)
        {
            //fetch threshold for the given tenent
            return new AlertThreshold();
        }

    }
}