using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Order
{
    public int Orderid { get; set; }

    public string? OrderDate { get; set; }

    public string? DeliveryDate { get; set; }

    public int? Pickpointid { get; set; }

    public int? Userid { get; set; }

    public string? Code { get; set; }

    public int? Statusid { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Pickpoint? Pickpoint { get; set; }

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
