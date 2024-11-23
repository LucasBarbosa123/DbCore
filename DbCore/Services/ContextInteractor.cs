
using DbCore.Dto;
using DbCoreDatabase.Data;
using DbCoreDatabase.Models;
using DbManagerService.Dto;

namespace DbCore.Services
{
    public class ContextInteractor : IContextInteractor
    {
        public void CreateDataBase(string name)
        {
			try
			{
                var newDb = new Dbasis
                {
                    Name = name,
                    CreationDate = DateTime.Now
                };

                using (var dbContext = new DbCoreContext())
                {
                    dbContext.Dbases.Add(newDb);
                    dbContext.SaveChanges();
                }
            }
			catch (Exception)
			{
				throw;
			}
        }

        public void CreateTable(string dbName, string tableName, List<Column> columns)
        {
            try
            {
                var dbId = GetDBIdByName(dbName);
                var newTable = new DbTable
                {
                    Name = tableName,
                    CreationDate = DateTime.Now,
                    DbId = dbId
                };

                using (var dbContext = new DbCoreContext())
                {
                    dbContext.DbTables.Add(newTable);
                    dbContext.SaveChanges();

                    var newTableColumns = columns.Select(c => new TableColumn
                    {
                        CreationDate= DateTime.Now,
                        DbTableId = newTable.Id,
                        Name = c.Name,
                        Type = c.Type
                    }).ToList();

                    dbContext.TableColumns.AddRange(newTableColumns);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int GetDBIdByName(string dbName)
        {
            try
            {
                using (var dbContext = new DbCoreContext())
                {
                    var db = dbContext.Dbases.Where(db => db.Name.ToLower() == dbName).FirstOrDefault();
                    return db.Id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
