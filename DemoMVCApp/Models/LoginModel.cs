using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMVCApp.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Role { get; set; }
        public int IsValid { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}