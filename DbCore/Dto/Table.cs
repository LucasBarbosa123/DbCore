using DbManagerService.Dto;

namespace DbCore.Dto
{
    public class CreateTableBody
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
    }
}
