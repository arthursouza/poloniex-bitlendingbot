using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BitLendingBot.UI.Startup))]
namespace BitLendingBot.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
