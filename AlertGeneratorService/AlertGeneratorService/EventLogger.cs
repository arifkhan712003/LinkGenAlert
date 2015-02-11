using System.Diagnostics;

namespace AlertGeneratorService
{
    public class EventLogger
    {
        internal static void LogAlert(LinkGenProcessedData processedData)
        {
            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = "LinkGenDownloadAlert";
                eventLog.WriteEntry("Download alert for tenent " + processedData.TenentId, EventLogEntryType.Information);
            }
        }
    }
}