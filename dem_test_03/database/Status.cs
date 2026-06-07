using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Status
{
    public int Statusid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
