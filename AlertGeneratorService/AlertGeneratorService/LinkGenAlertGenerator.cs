using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace AlertGeneratorService
{
    public class LinkGenAlertGenerator : IAlertGenerator
    {
        public void MonitorAlerts()
        {
            try
            {
                while (true)
                {
                    DateTime startTime = DateTime.Now;

                    int refreshIntervalInSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["refreshIntervalInSeconds"]);

                    ProcessAlerts(GetStartTimeForAlerts(startTime, refreshIntervalInSeconds), refreshIntervalInSeconds);

                    GoToSleep(startTime, refreshIntervalInSeconds);
                }

            }
            catch (Exception exception)
            {
                Console.Write(exception);
                throw;
            }
        }

        private static void GoToSleep(DateTime startTime, int refreshIntervalInSeconds)
        {
            int timeElapsed = DateTime.Now.Subtract(startTime).Seconds;

            if (refreshIntervalInSeconds > timeElapsed)
            {
                Thread.Sleep((refreshIntervalInSeconds - timeElapsed) * 1000);
            }
        }

        private void ProcessAlerts(DateTime startTime, int durationInSeconds)
        {
            int alertDurationInSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["alertDurationInSeconds"]);
            
            Dictionary<int, LinkGenProcessedData> linkGenProcessedDatas = new LinkGenDataProcessor().GetProcessedData(startTime,durationInSeconds);

            new LinkGenDbHelper().StoreAlertData(linkGenProcessedDatas);
            
            foreach (var linkGenProcessedData in linkGenProcessedDatas)
            {
                if (IsThresholdBreached(linkGenProcessedData.Key,alertDurationInSeconds))
                {
                    EventLogger.LogAlert(linkGenProcessedData.Value);
                }
            }
        }

        private static DateTime GetStartTimeForAlerts(DateTime startTime, int refreshIntervalInSeconds)
        {
            return startTime.AddSeconds(-refreshIntervalInSeconds);
        }

        private bool IsThresholdBreached(int tenentId, int alertDurationInSeconds)
        {
            LinkGenDbHelper linkGenDbHelper = new LinkGenDbHelper();

            LinkGenProcessedData alertData = linkGenDbHelper.FetchAlertData(tenentId, alertDurationInSeconds);

            IAlertThreshold alertThreshold = linkGenDbHelper.FetchAlertThreshold(tenentId);

            //do necessary comparrison
            return alertData.FileSizeInBytes > alertThreshold.AllowedVolume;
        }

    }
}