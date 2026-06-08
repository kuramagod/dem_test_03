using System;
using System.Collections.Generic;

namespace dem_test_03.database;

public partial class Product
{
    public int Productid { get; set; }

    public string? Article { get; set; }

    public string? Name { get; set; }

    public string? Unit { get; set; }

    public int? Price { get; set; }

    public int? Supplierid { get; set; }

    public int? Manufacturerid { get; set; }

    public int? Categoryid { get; set; }

    public int? Discount { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }

    public string BackgroundColor
    {
        get
        {
            if (Quantity == 0) return "LightBlue";
            if (Discount > 15) return "#2E8B57";
            return "Transponent";
        }
    }
}
