using ChatBotPrime.Core.Configuration;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Infra.Chat.Discord;
using ChatBotPrime.Infra.Chat.Twitch;
using ChatBotPrime.Infra.ChatHander;
using ChatBotPrime.Infra.SignalRCommunication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChatBotPrime.ConsoleApp
{
	class Program
	{
		private static IConfiguration Configuration { get; set; }

		static void Main(string[] args)
		{
			SetSignalRConfiguration(args);

			IServiceCollection services = new ServiceCollection();

			ConfigureServices(services);

			var sp = services.BuildServiceProvider();

			var chatServices = sp.GetServices<IChatService>();
			var ch = sp.GetService<ChatHandlerService>();
			var sr = sp.GetService<SignalRService>();


			Console.ReadLine();

			foreach (var svc in chatServices)
			{
				svc.Disconnect();
			}

		}

		private static void SetSignalRConfiguration(string[] args)
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
			ConfigureChatMessages(services);

			services.AddSingleton<ChatHandlerService>();
			services.AddSingleton<SignalRService>();

		}

		private static void ConfigureChatServices(IServiceCollection services,IConfigurationSection section)
		{
			var TwitchSettings = section.GetSection("TwitchSettings").Get<TwitchSettings>();
			var DiscordSettings = section.GetSection("DiscordSettings").Get<DiscordSettings>();

			if (TwitchSettings.Enabled) services.AddSingleton<IChatService, TwitchChatService>();
			if (DiscordSettings.Enabled) services.AddSingleton<IChatService, DiscordChatService>();

			ConfigureChatCommands(services);
		}

		private static void ConfigureChatCommands(IServiceCollection services)
		{
			var commands = Assembly.GetAssembly(typeof(IChatCommand)).GetTypes()
				.Where(x => x.Namespace == "ChatBotPrime.Core.Services.CommandHandler.Commands")
				.Where(x => x.IsClass)
				.Select(x => (IChatCommand)Activator.CreateInstance(x));


			services.AddSingleton(commands);
		}

		private static void ConfigureChatMessages(IServiceCollection services)
		{
			var messages = Assembly.GetAssembly(typeof(IChatMessage)).GetTypes()
				.Where(x => x.Namespace == "ChatBotPrime.Core.Services.CommandHandler.Messages")
				.Where(x => x.IsClass)
				.Select(x => (IChatMessage)Activator.CreateInstance(x));

			services.AddSingleton(messages);
		}
	}
}
