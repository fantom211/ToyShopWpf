using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyShopWpf.Helpers;
using ToyShopWpf.Models;

namespace ToyShopWpf
{
    public partial class AddEditViewModel : ObservableObject
    {
        private readonly ToyShopDbContext _context = new ToyShopDbContext();
        public Action<bool> CloseAction { get; set; }
        private Product? _product;
        private bool _isEditiing = false;

        [ObservableProperty]
        private string articul;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private ObservableCollection<Unit> units = new();

        [ObservableProperty]
        private Unit selectedUnit;

        [ObservableProperty]
        private decimal price;

        [ObservableProperty]
        private ObservableCollection<Provider> providers = new();
        [ObservableProperty]
        private Provider selectedProvider;

        [ObservableProperty]
        private ObservableCollection<Creator> creators = new();
        [ObservableProperty]
        private Creator selectedCreator;

        [ObservableProperty]
        private ObservableCollection<Category> categories = new();
        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private decimal discount;

        [ObservableProperty]
        private int amountInStock;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string photo;

        public AddEditViewModel(Product? product)
        {
            LoadData();
            if(product!=null)
            {
                _product = product;
                _isEditiing = true;
                InitializeForEditing();
            }  
        }

        private void LoadData()
        {
            Units = new ObservableCollection<Unit>(_context.Units.ToList());
            Providers = new ObservableCollection<Provider>(_context.Providers.ToList());
            Creators = new ObservableCollection<Creator>(_context.Creators.ToList());
            Categories = new ObservableCollection<Category>(_context.Categories.ToList());
        }

        private void InitializeForEditing()
        {
            Articul = _product.Articul;
            Name = _product.Name;

            SelectedUnit = Units
                .FirstOrDefault(x => x.Id == _product.UnitId);

            SelectedProvider = Providers
                .FirstOrDefault(x => x.Id == _product.ProviderId);

            SelectedCreator = Creators
                .FirstOrDefault(x => x.Id == _product.CreatorId);

            SelectedCategory = Categories
                .FirstOrDefault(x => x.Id == _product.CategoryId);

            Price = _product.Price;
            Discount = _product.Discount ?? 0;
            AmountInStock = _product.AmountInStock;
            Description = _product.Description ?? "";
            Photo = _product.Photo ?? "";
        }

        [RelayCommand]
        private void Cancel()
        {
            CloseAction?.Invoke(true);
        }

        [RelayCommand]
        private void Save()
        {
            if (!ValidateInput())
                return;

            if (_isEditiing == true)
            {
                if (UpdateProduct())
                {
                    new ProductWindow().Show();
                    CloseAction?.Invoke(true);
                }    
                    
                
            }
                
            else
            {
                if (CreateProduct())
                {             
                    new ProductWindow().Show();
                    CloseAction?.Invoke(true);
                }
            }

                
        }

        private bool CreateProduct()
        {
            var newProduct = new Product
            {
                Articul = Articul,
                Name = Name,
                UnitId = SelectedUnit.Id,
                ProviderId = SelectedProvider.Id,
                CreatorId = SelectedCreator.Id,
                CategoryId = SelectedCategory.Id,
                Price = Price,
                Discount = Discount,
                AmountInStock = AmountInStock,
                Description = Description,
                Photo = Photo
            };

            try
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                MessageHelper.ShowInfo("Продукт успешно добавлен");
                return true;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError(ex.ToString());
                return false;
            }
        }

        private bool UpdateProduct()
        {
            var product = _context.Products.FirstOrDefault(x=>x.Id == _product.Id);

            product.Articul = Articul;
            product.Name = Name;
            product.UnitId = SelectedUnit.Id;
            product.ProviderId = SelectedProvider.Id;
            product.CreatorId = SelectedCreator.Id;
            product.CategoryId = SelectedCategory.Id;
            product.Price = Price;
            product.Discount = Discount;
            product.AmountInStock = AmountInStock;
            product.Description = Description;
            product.Photo = Photo;

            try
            {
                _context.SaveChanges();
                MessageHelper.ShowInfo("Продукт успешно обновлён");
                return true;
            }
            catch(Exception ex)
            {
                MessageHelper.ShowError(ex.ToString());
                return false;
            }
        }

        private bool ValidateInput()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Articul))
                errors.AppendLine("Поле артикул не заполнено");
            if (string.IsNullOrWhiteSpace(Name))
                errors.AppendLine("Поле название не заполнено");
            if (string.IsNullOrWhiteSpace(Price.ToString())
                || Price < 0)
                errors.AppendLine("Поле цена не заполнено");
            if (string.IsNullOrWhiteSpace(Discount.ToString())
                ||Discount > 100 || Discount < 0)
                errors.AppendLine("Поле скидка не заполнено");
            if (string.IsNullOrWhiteSpace(AmountInStock.ToString())
                || AmountInStock < 0)
                errors.AppendLine("Поле количество на складе не заполнено");
            if (string.IsNullOrWhiteSpace(Description))
                errors.AppendLine("Поле описание не заполнено");

            if(errors.Length > 0)
            {
                MessageHelper.ShowError(errors.ToString());
                return false;
            }
            return true;
        }
    }
}
