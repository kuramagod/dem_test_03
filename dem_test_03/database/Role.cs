using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Role
{
    public int Roleid { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
