using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcProjectBlogPage.Startup))]
namespace MvcProjectBlogPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
