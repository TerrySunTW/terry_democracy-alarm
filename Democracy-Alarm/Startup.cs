using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Democracy_Alarm.Startup))]
namespace Democracy_Alarm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
