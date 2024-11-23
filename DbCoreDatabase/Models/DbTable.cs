using System;
using System.Collections.Generic;

namespace DbCoreDatabase.Models;

public partial class DbTable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? DbId { get; set; }
}
