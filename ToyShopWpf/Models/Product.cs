using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

namespace ToyShopWpf.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Articul { get; set; } = null!;

    public string? Name { get; set; }

    public int UnitId { get; set; }

    public decimal Price { get; set; }

    public int ProviderId { get; set; }

    public int CreatorId { get; set; }

    public int CategoryId { get; set; }

    public decimal? Discount { get; set; }

    public int AmountInStock { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Creator Creator { get; set; } = null!;

    public virtual ICollection<ProductInOrder> ProductInOrders { get; set; } = new List<ProductInOrder>();

    public virtual Provider Provider { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;

    [NotMapped]
    public Brush Background
    {
        get
        {
            if (Discount >= 17)
                return (Brush)new BrushConverter().ConvertFromString("#FFDEAD");

            if (AmountInStock == 0)
                return Brushes.LightBlue;

            return (Brush)new BrushConverter().ConvertFromString("#F5DEB3");
        }
    }

    [NotMapped]
    public string PhotoSource
    {
        get => (string.IsNullOrEmpty(Photo) || Photo == "") ? "/Res/picture.png" : Photo;
    }

    [NotMapped]
    public decimal DiscountPrice
    {
        get
        {
            if (!Discount.HasValue || Discount == 0)
            {
                return Price;
            }
            return Price * (1 - Discount.Value / 100);
        }
    }
}
