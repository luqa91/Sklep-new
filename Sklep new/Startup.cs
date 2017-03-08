using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Sklep_new.Startup))]
namespace Sklep_new
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration
    .UseSqlServerStorage("KursyContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }
    }
}