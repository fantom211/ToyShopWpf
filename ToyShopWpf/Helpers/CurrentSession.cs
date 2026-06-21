using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyShopWpf.Models;

namespace ToyShopWpf.Helpers
{
    public static class CurrentSession
    {
        public static User CurrentUser { get; set; }
    }
}
