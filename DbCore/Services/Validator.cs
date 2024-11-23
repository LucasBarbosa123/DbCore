using DbCoreDatabase.Data;
using DbManagerService.Dto;

namespace DbCore.Services
{
    public class Validator : IValidator
    {
        //
        //  DATABASES
        //
        public bool ValidDbName(string dbName, out string reason)
        {
            reason = "";
            if (string.IsNullOrEmpty(dbName) || dbName.Contains(" "))
            {
                reason = "The dbName argument needs to be filled and can't have spaces.";
                return false;
            }
            if (DbNameAlreadyExists(dbName))
            {
                reason = "Already exists a database with that name.";
                return false;
            }
            if (IsReservedWord(dbName))
            {
                reason = "That is a reserved word.";
                return false;
            }
            if (!ValidName(dbName))
            {
                reason = "That name is invalid.";
                return false;
            }

            return true;
        }

        private bool DbNameAlreadyExists(string dbName)
        {
            using (var dbContext = new DbCoreContext())
            {
                var dbNames = dbContext.Dbases.Where(db => db.Name.ToUpper() == dbName.ToUpper()).FirstOrDefault();
                return dbNames != null;
            }
        }

        //
        //  TABLES
        //
        public bool ValidTable(string dbName, string tableName, List<Column> columns, out string reason)
        {
            reason = "";

            if (!DbNameAlreadyExists(dbName))
            {
                reason = "Database doesn't found.";
                return false;
            }

            if (string.IsNullOrEmpty(tableName) || tableName.Contains(" "))
            {
                reason = "The tableName argument needs to be filled and can't have spaces.";
                return false;
            }
            if (TableAlreadyExists(dbName, tableName))
            {
                reason = "Already exists a table in this database with that name.";
                return false;
            }
            if (IsReservedWord(tableName))
            {
                reason = "That is a reserved word.";
                return false;
            }
            if (!ValidName(tableName))
            {
                reason = "That name is invalid.";
                return false;
            }

            //need to validate the columns
            if (!ValidColumnNames(columns, out reason))
            {
                return false;
            }

            return true;
        }

        private bool TableAlreadyExists(string dbName, string tableName)
        {
            using (var dbContext = new DbCoreContext())
            {
                var db = dbContext.Dbases.Where(b => b.Name == dbName).FirstOrDefault();
                if (db != null)
                {
                    return dbContext.DbTables.Where(t => t.Name == tableName).FirstOrDefault() != null;
                }

                return false;
            }
        }

        //
        //  COLUMNS
        //
        private bool ValidColumnNames(List<Column> columns, out string reason)
        {
            reason = "";

            var nullOrEmptys = columns.Where(c => (string.IsNullOrEmpty(c.Name) || c.Name.Contains(" "))).FirstOrDefault();
            if (nullOrEmptys != null)
            {
                reason = "All column names need to be filled and can't have spaces";
                return false;
            }

            var reservedWords = columns.Where(c => IsReservedWord(c.Name)).FirstOrDefault();
            if (reservedWords != null)
            {
                reason = "One of the column names is a reserved word.";
                return false;
            }

            //!ValidName(tableName)
            var invalidNames = columns.Where(c => !ValidName(c.Name)).FirstOrDefault();
            if (invalidNames != null)
            {
                reason = "One of the column names is invalid.";
                return false;
            }

            return true;
        }

        //
        //  GENERAL VALIDATIONS
        //
        private bool ValidName(string s)
        {
            if (s.LastOrDefault() == ']' && s.FirstOrDefault() != '[' || s.FirstOrDefault() == '[' && s.LastOrDefault() != ']')
            {
                return false;
            }

            return !IsFirstCharInt(s) && !HasReservedCharacter(s);
        }

        private bool IsFirstCharInt(string s) => FIRST_TEN_INTS.Contains(s.FirstOrDefault());
        private List<char> FIRST_TEN_INTS = new List<char> 
        { 
            '0', '1', '2', '3', '4', 
            '5', '6', '7', '8', '9' 
        };

        private bool HasReservedCharacter(string s) => (s.Where(c => INVALID_CHARACTERS.Contains(c)).FirstOrDefault() != '\0');     //\0 is a null char
        private List<char> INVALID_CHARACTERS = new List<char>
        {
            '@',  '#',  '$',  '%',  '^',  '&',
            '*',  '(',  ')',  '-',  '+',  '=',
            '{',  '}',  '|',  '\\',
            ':',  ';',  '"',  '\'', '<',  '>',
            ',',  '.',  '?',  '/'
        };

        private bool IsReservedWord(string s) => RESERVED_WORDS.Contains(s.ToUpper());
        private List<string> RESERVED_WORDS = new List<string>
        {
            "ADD", "EXTERNAL", "ORDER", "TOP",
            "ALL", "FETCH", "OUTER", "TRAN",
            "ALTER", "FILE", "OVER", "TRANSACTION",
            "AND", "FILLFACTOR", "PERCENT", "TRIGGER",
            "ANY", "FOR", "PLAN", "TRUNCATE",
            "AS", "FOREIGN", "PRIMARY", "TRY",
            "ASC", "FREETEXT", "PRINT", "UNION",
            "AUTHORIZATION", "FREETEXTTABLE", "PROC", "UNIQUE",
            "AVG", "FROM", "PROCEDURE", "UPDATE",
            "BACKUP", "FULL", "PUBLIC", "UPDATETEXT",
            "BEGIN", "FUNCTION", "RAISERROR", "USE",
            "BETWEEN", "GOTO", "READ", "USER",
            "BREAK", "GRANT", "READTEXT", "VALUES",
            "BROWSE", "GROUP", "RECONFIGURE", "VARYING",
            "BULK", "HAVING", "REFERENCES", "VIEW",
            "BY", "HOLDLOCK", "REPLICATION", "WAITFOR",
            "CASCADE", "IDENTITY", "RESTORE", "WHEN",
            "CASE", "IF", "RESTRICT", "WHERE",
            "CHECK", "IN", "RETURN", "WHILE",
            "CHECKPOINT", "INDEX", "REVERT", "WITH",
            "CLOSE", "INNER", "REVOKE", "WRITETEXT",
            "CLUSTERED", "INSERT", "RIGHT", "ABSOLUTE",
            "COALESCE", "INTERSECT", "ROLLBACK", "ACROSS",
            "COLUMN", "INTO", "ROWCOUNT", "ACTION",
            "COMMIT", "IS", "ROWGUIDCOL", "ADDITIONAL",
            "COMPUTE", "JOIN", "RULE", "ADMIN",
            "CONSTRAINT", "KEY", "SAVE", "AFTER",
            "CONTAINS", "KILL", "SCHEMA", "AGGREGATE",
            "CONTAINSTABLE", "LEFT", "SELECT", "ALIAS",
            "CONTINUE", "LIKE", "SESSION_USER", "ALL_ROWS",
            "CONVERT", "LINENO", "SET", "ALLOCATE",
            "CREATE", "LOAD", "SETUSER", "ALTERED",
            "CROSS", "MERGE", "SHUTDOWN", "ALWAYS",
            "CURRENT", "NATIONAL", "SOME", "ANALYZE",
            "CURRENT_DATE", "NOCHECK", "STATISTICS", "AND_EQUAL",
            "CURRENT_TIME", "NONCLUSTERED", "SYSTEM_USER", "ANYALL",
            "CURRENT_TIMESTAMP", "NOT", "TABLE", "ARRAY",
            "CURRENT_USER", "NULL", "TEXTSIZE", "ASENSITIVE",
            "CURSOR", "NULLIF", "THEN", "ASYNC",
            "DATABASE", "OF", "TO", "ATOMIC",
            "DBCC", "OFF", "TOP", "ATTACH",
            "DEALLOCATE", "OFFSETS", "TRANSACTION", "AUDIT",
            "DECLARE", "ON", "TRIGGER", "AUTO",
            "DEFAULT", "OPEN", "TRUNCATE", "AUTOALLOCATE",
            "DELETE", "OPENDATASOURCE", "TSEQUAL", "AUTOHINT",
            "DENY", "OPENQUERY", "UNION", "AVAILABILITY",
            "DESC", "OPENROWSET", "UNIQUE", "BACKWARD",
            "DISK", "OPENXML", "UNPIVOT", "BEGIN",
            "DISTINCT", "OPTION", "UPDATE", "BINARY",
            "DISTRIBUTED", "OR", "USE", "BIND",
            "DOUBLE", "ORDER", "VALUE", "BLOCKED",
            "DROP", "OUTER", "VALUES", "BOOLEAN",
            "DUMP", "OVER", "VARYING", "BOOST",
            "ELSE", "OVERLAPS", "VIEW", "BRANCH",
            "END", "PAGELATCH", "WAITFOR", "BROADCAST",
            "ERRLVL", "PARTIAL", "WHEN", "BROWSEABLE",
            "ESCAPE", "PERCENT", "WHERE", "BUFFERPOOL",
            "EXCEPT", "PLAN", "WHILE", "BULK_LOGGED",
            "EXEC", "POLICY", "WINDOW", "BURIED",
            "EXECUTE", "PRECISION", "WITH", "CACHE",
            "EXISTS", "PRIMARY", "WITHIN", "CACHE_HIT",
            "EXIT", "PRINT", "WRITETEXT", "CALLABLE"
        };
    }
}
