using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Supplier
{
    public int Supplierid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
