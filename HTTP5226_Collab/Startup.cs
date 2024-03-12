using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HTTP5226_Collab.Startup))]
namespace HTTP5226_Collab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
