using System;
using System.Collections.Generic;

namespace DbCoreDatabase.Models;

public partial class TableColumn
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? DbTableId { get; set; }
}
