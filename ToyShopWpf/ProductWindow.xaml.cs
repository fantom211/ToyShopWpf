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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            ProductViewModel vm = new ProductViewModel();
            DataContext = vm;
            vm.CloseAction = result =>
            {
                Close();
            };
        }

        public ProductWindow(User user)
        {
            InitializeComponent();
            ProductViewModel vm = new ProductViewModel();
            DataContext = vm;
            vm.CloseAction = result =>
            {
                Close();
            };
        }
    }
}
