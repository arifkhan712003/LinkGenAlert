using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AlertGeneratorService
{
    class AlertGenerator
    {
        static void Main(string[] args)
        {

            //2014070405
            //DateTime startDt = new DateTime(2015, 02, 11, 9, 0, 0);
            DateTime startDt = new DateTime(2014, 07, 04, 05, 0, 0);
            IList<CdnRawData> upfos = new AzureAccess().FetchData(startDt, 60);

            foreach (var upfo in upfos)
            {
                Console.WriteLine(upfo.Timestamp + " " + upfo.PartitionKey + ">> " + upfo.ClientInformation_Name);
            }

            Console.WriteLine(upfos.Count);

            //List<LinkGenRawData> rawDatas = new List<LinkGenRawData>();
            //rawDatas.Add(new LinkGenRawData(){ SubscriberId = 1, FileName = "file1", FileSizeInBytes = 11,IpAddress = "1.1.1.1"});
            //rawDatas.Add(new LinkGenRawData() { SubscriberId = 1, FileName = "file1", FileSizeInBytes = 12, IpAddress = "1.1.1.1" });
            //rawDatas.Add(new LinkGenRawData() { SubscriberId = 1, FileName = "file1", FileSizeInBytes = 13, IpAddress = "1.1.1.1" });
            //rawDatas.Add(new LinkGenRawData() { SubscriberId = 2, FileName = "file1", FileSizeInBytes = 10, IpAddress = "1.1.1.1" });
            //rawDatas.Add(new LinkGenRawData() { SubscriberId = 2, FileName = "file1", FileSizeInBytes = 10, IpAddress = "1.1.1.1" });
            //rawDatas.Add(new LinkGenRawData() { SubscriberId = 3, FileName = "file1", FileSizeInBytes = 10, IpAddress = "1.1.1.1" });

            //List<AttributeData> obj = GetFileCountAttribute(rawDatas);
            //obj.AddRange(GetDownloadVolumeAttribute(rawDatas));

            //foreach (AttributeData linkGenProcessedData in obj)
            //{
            //    Console.WriteLine(linkGenProcessedData.SubscriberId +" "+ linkGenProcessedData.AttributeId +" "+ linkGenProcessedData.AttributeValue +" "+ linkGenProcessedData.StartDate);
            //}

            //using (DownloadDataContext dbContext = new DownloadDataContext())
            //{
            //    foreach (AttributeData attributeData in obj)
            //    {
            //        var alertExist = (from x in dbContext.AttributeDataDbSet
            //            where (x.SubscriberId == attributeData.SubscriberId && x.StartDate == DateTime.Now)
            //            select x).FirstOrDefault();

            //        if (alertExist == null)
            //        {
            //            dbContext.AttributeDataDbSet.Add(attributeData);
            //        }
            //        else
            //        {

            //            dbContext.Entry(attributeData).State = EntityState.Modified;
            //        }

            //        dbContext.SaveChanges();

            //    }
            //}

            //var linkGenAlertThread = StartLinkGenAlertThread();

            //linkGenAlertThread.Join();
        }


        private static List<AttributeData> GetFileCountAttribute(List<LinkGenRawData> rawDatas)
        {
            return (from rawData in rawDatas
                group rawData by rawData.SubscriberId
                into rawGrouping
                select
                    new AttributeData()
                    {
                        SubscriberId = rawGrouping.Key,
                        AttributeId = 1,
                        AttributeValue = rawGrouping.Count(),
                        StartDate = DateTime.Now
                    }).ToList();
        }

        private static List<AttributeData> GetDownloadVolumeAttribute(List<LinkGenRawData> rawDatas)
        {
            var obj = from rawData in rawDatas
                      group rawData by rawData.SubscriberId
                          into rawGrouping
                          select
                              new AttributeData()
                              {
                                  SubscriberId = rawGrouping.Key,
                                  AttributeId = 2,
                                  AttributeValue = rawGrouping.Sum(x => x.FileSizeInBytes)
                              };

            return obj.ToList();
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
