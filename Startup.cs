using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeTheCloud.Startup))]
namespace CodeTheCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
