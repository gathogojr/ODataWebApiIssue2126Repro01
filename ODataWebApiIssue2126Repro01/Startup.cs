using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using ODataWebApiIssue2126Repro01.Models;
using System.Linq;

namespace ODataWebApiIssue2126Repro01
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().Expand().Count().OrderBy().SkipToken().MaxTop(100);
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.EnableDependencyInjection();
            });
        }

        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Customer>("Customers");
            builder.ComplexType<Address>();

            return builder.GetEdmModel();
        }
    }
}
