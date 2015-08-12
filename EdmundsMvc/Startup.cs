using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EdmundsMvc.Startup))]
namespace EdmundsMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
