using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class OrderDetail
{
    public int? Productid { get; set; }

    public int? Orderid { get; set; }

    public int Orderdetailid { get; set; }

    public int? Quantity { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
