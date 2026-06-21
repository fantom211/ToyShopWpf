using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ToyShopWpf.Helpers;
using ToyShopWpf.Models;

namespace ToyShopWpf
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ToyShopDbContext _context = new ToyShopDbContext();
        private readonly ProductService _productService = new ProductService();

        [ObservableProperty]
        private string userFullName = CurrentSession.CurrentUser.FullName;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        public Action<bool> CloseAction { get; set; }

        [ObservableProperty]
        private Brush background;
        public ProductViewModel()
        {
            LoadProduct();
        }

        private void LoadProduct()
        {
            Products = new ObservableCollection<Product>(
                _productService.GetAllProducts());
        }

        [RelayCommand]
        private void LogOut()
        {
            CurrentSession.CurrentUser = null;
            new MainWindow().Show();
            CloseAction?.Invoke(true);
        }
    }
}
