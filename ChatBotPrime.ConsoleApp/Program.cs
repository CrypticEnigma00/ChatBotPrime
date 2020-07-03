using ChatBotPrime.Core.Configuration;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Infra.Chat.Discord;
using ChatBotPrime.Infra.Chat.Twitch;
using ChatBotPrime.Infra.CommandHander;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;


namespace ChatBotPrime.ConsoleApp
{
	class Program
	{
		private static IConfiguration Configuration { get; set; }

		static void Main(string[] args)
		{
			SetConfiguration(args);

			IServiceCollection services = new ServiceCollection();

			ConfigureServices(services);

			var sp = services.BuildServiceProvider();

			IEnumerable<IChatService> chatServices = sp.GetServices<IChatService>();
			CommandHandlerService ch = sp.GetService<CommandHandlerService>();


			Console.ReadLine();

			foreach (var svc in chatServices)
			{
				svc.Disconnect();
			}

		}

		private static void SetConfiguration(string[] args)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", true, true)
				.AddUserSecrets<Program>()
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			var section = Configuration.GetSection("AppSettings");

			services.Configure<ApplicationSettings>(section);

			services.AddLogging(configure => configure.AddConsole());

			ConfigureChatServices(services, section);

			services.AddSingleton<CommandHandlerService>();

		}

		private static void ConfigureChatServices(IServiceCollection services,IConfigurationSection section)
		{
			var TwitchSettings = section.GetSection("TwitchSettings").Get<TwitchSettings>();
			var DiscordSettings = section.GetSection("DiscordSettings").Get<DiscordSettings>();

			if (TwitchSettings.Enabled)
			{
				services.AddSingleton<IChatService, TwitchChatService>();
			}

			if (DiscordSettings.Enabled)
			{
				services.AddSingleton<IChatService, DiscordChatService>();
			}
		}
	}
}
