using System;

namespace AlertGeneratorService
{
    public class LinkGenDbHelper
    {

        internal void StoreAlertData(System.Collections.Generic.Dictionary<int, AttributeData> linkGenProcessedDatas)
        {
            foreach (var linkGenProcessedData in linkGenProcessedDatas)
            {
                //insert multiple rows
                StoreAlertData(linkGenProcessedData.Value);
            }
        }

        private void StoreAlertData(AttributeData processedData)
        {
            throw new NotImplementedException();
        }

        internal AttributeData FetchAlertData(int tenentId, int alertDurationInSeconds)
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