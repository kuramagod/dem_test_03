using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class User
{
    public int Userid { get; set; }

    public string? Fullname { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Roleid { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
