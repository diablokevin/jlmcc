using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JLMCC.Startup))]
namespace JLMCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
