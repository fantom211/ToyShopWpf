using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyShopWpf.Helpers;
using ToyShopWpf.Models;

namespace ToyShopWpf
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ToyShopDbContext _context = new ToyShopDbContext();

        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        public Action<bool> CloseAction { get; set; }

        [RelayCommand]
        private void LogIn()
        {
            var user = _context.Users.Where(u=>u.Login == Login && u.Password == Password).FirstOrDefault();

            if (user == null)
            {
                MessageHelper.ShowError("Логин или пароль неверные");
                return;
            }

            else
            {
                CurrentSession.CurrentUser = user;
                new ProductWindow(user).Show();
                CloseAction?.Invoke(true);
            }
        }

        [RelayCommand]
        private void LogInUn()
        {
            new ProductWindow().Show();
        }
    }
}
