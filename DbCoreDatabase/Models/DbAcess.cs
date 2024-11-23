using System;
using System.Collections.Generic;

namespace DbCoreDatabase.Models;

public partial class DbAcess
{
    public int Id { get; set; }

    public int? DbId { get; set; }

    public int? UserId { get; set; }
}
