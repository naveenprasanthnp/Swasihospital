using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using SwasiHealthCare.Service.Controllers;

[assembly: OwinStartup(typeof(SwasiHealthCare.Service.Startup))]

namespace SwasiHealthCare.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var cretesuperadmin = new HomeController();
            cretesuperadmin.CreateSuperAdmin();
        }
    }
}
