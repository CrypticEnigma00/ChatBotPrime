using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(ChatBotPrime.FrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace ChatBotPrime.FrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}