using System;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;

namespace SampleWebApi
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            InitContainer(config);
            InitRoute(config);
            InitFormatters(config);
            config.EnsureInitialized();
        }

        static void InitFormatters(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }

        void InitRoute(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("message", "message", new {controller = "Message"});
            config.Routes.MapHttpRoute("complex", "complex", new {controller = "ComplexContract"});
        }

        static void InitContainer(HttpConfiguration config)
        {
            var cb = new ContainerBuilder();
            cb.RegisterApiControllers(typeof(HttpApplication).Assembly);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                cb.Build());
        }
    }
}