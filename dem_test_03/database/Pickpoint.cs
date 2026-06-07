using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Pickpoint
{
    public int Pickpointid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
