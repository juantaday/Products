using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.ViewModels
{
   public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        #endregion

        #region constructor
        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
        #endregion
    }
}
