using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace AlertGeneratorService
{
    class CdnRawData : TableEntity
    {
        public string DSLinkGenRequest_EndUserIPAddress { get; set; }

        public string ClientInformation_Name { get; set; }
    }
}
