using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EndecoDemo2017.Startup))]
namespace EndecoDemo2017
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
