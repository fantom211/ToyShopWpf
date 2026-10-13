using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToyShopWpf.Models;

namespace ToyShopWpf
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        public AddEditProductWindow(Product product)
        {
            InitializeComponent();
            var vm = new AddEditViewModel(product);
            DataContext = vm;
            vm.CloseAction = result =>
            {
                Close();
            };
        }
    }
}
