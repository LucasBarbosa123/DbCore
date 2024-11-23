using DbManagerService.Dto;

namespace DbCore.Services
{
    public interface IContextInteractor
    {
        void CreateDataBase(string name);
        void CreateTable(string dbName, string tableName, List<Column> columns);
    }
}
