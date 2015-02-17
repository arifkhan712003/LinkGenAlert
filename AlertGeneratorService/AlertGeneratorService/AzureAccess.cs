using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace AlertGeneratorService
{
    class AzureAccess
    {
        private string connectionString;

        public AzureAccess()
        {
            connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
        }

        List<String> diagnosticTables = new List<string>()
            {
                "WADDiagnosticInfrastructureLogsTable",
                "WADDirectoriesTable",
                "WADLogsTable",
                "WADWindowsEventLogsTable"
            };

        //string accountName = "dsstlinkgeneusstore";
        //string accountKey = "RVJz9ZdamwfcfwSWGGz7QpeYSjbVY4pQqLC7yqVpfmAL57jkl+1UV+EZIR9zvsIsORU8Q64TxfFsJMO1DsbFLQ==";

        public IList<CdnRawData> FetchData(DateTime startDt, int durationInMinutes)
        {
            List<CdnRawData> cdnRawDatas = new List<CdnRawData>();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            IList<String> tenentTables = FetchTenentTables();

            foreach (var tenentTable in tenentTables)
            {
                CloudTable table = tableClient.GetTableReference(tenentTable);

                TableQuery<CdnRawData> query = new TableQuery<CdnRawData>().Where(GetFilterCondition(startDt, durationInMinutes));

                cdnRawDatas.AddRange(table.ExecuteQuery(query).ToList());
            }

            return cdnRawDatas;
        }

        IList<string> FetchTenentTables()
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            IEnumerable<CloudTable> tableList = tableClient.ListTables();

            List<string> tableNames = tableList.Select(ct => ct.Name).ToList();

            tableNames.RemoveAll(x => diagnosticTables.Contains(x));

            return tableNames;
        }

        private static string GetFilterCondition(DateTime startDt, int durationInMinutes)
        {
            startDt = DateTime.SpecifyKind(startDt, DateTimeKind.Utc);

            string partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                startDt.ToString("yyyyMMddHH"));

            DateTimeOffset startOffset = startDt;

            DateTimeOffset endOffSet = startDt.AddMinutes(durationInMinutes);

            string startTime = TableQuery.GenerateFilterConditionForDate("Timestamp",
                QueryComparisons.GreaterThanOrEqual, startOffset);
            string endtime = TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThanOrEqual,
                endOffSet);

            string filterCondition = TableQuery.CombineFilters(startTime, TableOperators.And, endtime);
            filterCondition = TableQuery.CombineFilters(partitionKeyFilter, TableOperators.And, filterCondition);
            return filterCondition;
        }

    }
}
