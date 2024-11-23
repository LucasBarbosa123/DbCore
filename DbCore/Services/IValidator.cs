using DbManagerService.Dto;

namespace DbCore.Services
{
    public interface IValidator
    {
        bool ValidDbName(string dbName, out string reason);
        bool ValidTable(string dbName, string tableName, List<Column> columns, out string reason);
    }
}
