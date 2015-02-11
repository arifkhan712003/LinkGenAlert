using System;
using System.Threading;

namespace AlertGeneratorService
{
    class AlertGenerator
    {
        static void Main(string[] args)
        {
            var linkGenAlertThread = StartLinkGenAlertThread();

            linkGenAlertThread.Join();
        }

        private static Thread StartLinkGenAlertThread()
        {
            IAlertGenerator linkGenAlert = new LinkGenAlertGenerator();

            Thread thread = new Thread(linkGenAlert.MonitorAlerts);
            thread.Start();

            return thread;
        }
    }
}
