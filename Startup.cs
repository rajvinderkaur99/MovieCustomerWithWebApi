using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovCustMVCAppWithAuthen.Startup))]
namespace MovCustMVCAppWithAuthen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
