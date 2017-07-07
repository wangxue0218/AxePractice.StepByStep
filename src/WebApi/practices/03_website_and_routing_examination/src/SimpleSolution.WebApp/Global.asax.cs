using System;
using System.Web;
using System.Web.Http;

namespace SimpleSolution.WebApp
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            Bootstrapper.Init(httpConfiguration);
        }
    }
}