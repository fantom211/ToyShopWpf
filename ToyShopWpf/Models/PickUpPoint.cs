using System;
using System.Collections.Generic;

namespace ToyShopWpf.Models;

public partial class PickUpPoint
{
    public int Id { get; set; }

    public int Postcode { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string? Building { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
