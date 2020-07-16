using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatBotPrime.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.UseEnvironment("Development")
			.ConfigureServices((hostContext, services) =>
			{
				services
					.ConfigureApplicationServices(hostContext.Configuration,args)
					.AddHostedService<ChatBotPrimeService>();
			});
	}
}
