using System;
using System.Collections.Generic;

namespace SimpleRESTfulAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime DreatedDate { get; set; }

    public decimal Total { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
