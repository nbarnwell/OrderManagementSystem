using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DddCqrsEsExample.Web2.Startup))]
namespace DddCqrsEsExample.Web2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
