using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterDetailsDemo.Startup))]
namespace MasterDetailsDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
