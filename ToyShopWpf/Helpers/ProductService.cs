using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyShopWpf.Models;

namespace ToyShopWpf.Helpers
{
    public class ProductService
    {
        private readonly ToyShopDbContext _context = new ToyShopDbContext();

        public ObservableCollection<Product> GetAllProducts()
        {
            return new ObservableCollection<Product>(_context.Products
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Provider)
                .Include(p => p.Unit).ToList());
        }
    }
}
