using System.Diagnostics;

namespace AlertGeneratorService
{
    public class EventLogger
    {
        internal static void LogAlert(AttributeData processedData)
        {
            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = "LinkGenDownloadAlert";
                eventLog.WriteEntry("Download alert for tenent " + processedData.SubscriberId, EventLogEntryType.Information);
            }
        }
    }
}