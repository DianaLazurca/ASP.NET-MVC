using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineEvaluator.Startup))]
namespace OnlineEvaluator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
