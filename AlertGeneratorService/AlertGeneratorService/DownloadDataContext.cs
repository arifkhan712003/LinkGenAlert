using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AlertGeneratorService
{
    class DownloadDataContext : DbContext
    {
        public virtual DbSet<AttributeData> AttributeDataDbSet { get; set; }
    }
}
