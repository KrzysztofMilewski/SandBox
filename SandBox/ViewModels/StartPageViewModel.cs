using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SandBox.Models;

namespace SandBox.ViewModels
{
    public class StartPageViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}