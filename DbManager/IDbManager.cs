using DbManagerService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagerService
{
    public interface IDbManager
    {
        void CreateDatabase(string name);
        void CreateTable(string dbName, string tableName, List<Column> columns);
    }
}
