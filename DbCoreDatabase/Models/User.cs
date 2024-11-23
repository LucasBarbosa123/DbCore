using System;
using System.Collections.Generic;

namespace DbCoreDatabase.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Pass { get; set; }

    public bool? Blocked { get; set; }
}
