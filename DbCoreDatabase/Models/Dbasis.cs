using System;
using System.Collections.Generic;

namespace DbCoreDatabase.Models;

public partial class Dbasis
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreationDate { get; set; }
}
