using System;
using System.Collections.Generic;

namespace ToyShopWpf.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int PickUpPointId { get; set; }

    public int UserId { get; set; }

    public int Code { get; set; }

    public int OrderStatus { get; set; }

    public virtual OrderStatus OrderStatusNavigation { get; set; } = null!;

    public virtual PickUpPoint PickUpPoint { get; set; } = null!;

    public virtual ICollection<ProductInOrder> ProductInOrders { get; set; } = new List<ProductInOrder>();

    public virtual User User { get; set; } = null!;
}
