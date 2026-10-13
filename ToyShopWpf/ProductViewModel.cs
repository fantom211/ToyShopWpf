using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ToyShopWpf.Helpers;
using ToyShopWpf.Models;

namespace ToyShopWpf
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ToyShopDbContext _context = new ToyShopDbContext();
        private readonly ProductService _productService = new ProductService();

        public ICollectionView ProductsView { get; }

        [ObservableProperty]
        private string userFullName;

        [ObservableProperty]
        private ObservableCollection<Product> products = new();

        [ObservableProperty]
        private ObservableCollection<string> sortingTypes = new()
        {
            "По умолчанию",
            "По убыванию",
            "По возрастанию"
        };

        [ObservableProperty]
        private string selectedSort;

        [ObservableProperty]
        private ObservableCollection<string> filteringTypes = new()
        {
            "Все поставщики"
        };

        [ObservableProperty]
        private string selectedFilter;

        [ObservableProperty]
        private Product selectedItem;

        [ObservableProperty]
        private string searchingProduct;

        public Action<bool> CloseAction { get; set; }

        [ObservableProperty]
        private Brush background;
        public ProductViewModel()
        {
            LoadProduct();

            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = FilterProducts;

            LoadData();
            LoadUI();
        }

        private void LoadUI()
        {
            User user = CurrentSession.CurrentUser;
        }

        private void LoadData()
        {
            var providers = _context.Providers.ToList();
            foreach (var p in providers)
                FilteringTypes.Add(p.Name);

            User user = CurrentSession.CurrentUser;
            if(user!= null)
                UserFullName = user.FullName;

            SelectedFilter = FilteringTypes[0];
            SelectedSort = SortingTypes[0];
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

        private bool FilterProducts(object obj)
        {
            if (obj is not Product product)
                return false;

            bool nameMatch = 
                string.IsNullOrWhiteSpace(SearchingProduct) ||
                product.Name.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase)
                || product.Category.Name.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase)
                || product.Provider.Name.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase)
                || product.Creator.Name.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase)
                || product.Unit.Name.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase)
                || product.Description!.Contains(SearchingProduct, StringComparison.OrdinalIgnoreCase);

            bool providerMatch =
                SelectedFilter == "Все поставщики"
                || product.Provider.Name == SelectedFilter;

            return nameMatch && providerMatch;
        }

        partial void OnSearchingProductChanged(string value)
        {
            ProductsView.Refresh();
        }

        partial void OnSelectedSortChanged(string value)
        {
            ProductsView.SortDescriptions.Clear();
            switch (value)
            {
                case "По умолчанию":
                    break;

                case "По убыванию":
                    ProductsView.SortDescriptions.Add(
                        new SortDescription(nameof(Product.AmountInStock),
                        ListSortDirection.Descending));
                    break;
                case "По возрастанию":
                    ProductsView.SortDescriptions.Add(
                        new SortDescription(nameof(Product.AmountInStock),
                        ListSortDirection.Ascending));
                    break;

            }
        }

        partial void OnSelectedFilterChanged(string value)
        {
            ProductsView.Refresh();
        }

        [RelayCommand]
        private void AddProduct()
        {
            new AddEditProductWindow(null).Show();
        }
        [RelayCommand]
        private void EditProduct()
        {
            new AddEditProductWindow(SelectedItem).Show();
            CloseAction?.Invoke(true);
        }
    }
}
