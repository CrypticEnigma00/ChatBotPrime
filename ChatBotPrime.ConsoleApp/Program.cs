using ChatBotPrime.Core.Configuration;
using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Infra.Chat.Discord;
using ChatBotPrime.Infra.Chat.Twitch;
using ChatBotPrime.Infra.ChatHander;
using ChatBotPrime.Infra.Data.EF;
using ChatBotPrime.Infra.SignalRCommunication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChatBotPrime.ConsoleApp
{
	class Program
	{
		private static IConfiguration _configuration { get; set; }

		static void Main(string[] args)
		{

			SetupConfiguration(args);

			//Configure and instanciate the Service provider.
			var sp = ConfigureServices().BuildServiceProvider();

			//Configure and migrate the DB before it is used.
			var repo = sp.GetService<IRepository>();
			var appDataContext = sp.GetService<AppDataContext>();
			SetupDatabase.Configure(appDataContext, repo);


			//Instanciate the required services for running.
			var chatServices = sp.GetServices<IChatService>();
			var ch = sp.GetService<ChatHandlerService>();
			var sr = sp.GetService<SignalRService>();


			//Hold console open
			Console.ReadLine();

			//Dispose of services before application exit.
			foreach (var svc in chatServices)
			{
				svc.Disconnect();
			}
		}

		private static void SetupConfiguration(string[] args)
		{
			_configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", true, true)
				.AddUserSecrets<Program>()
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();
		}

		private static IServiceCollection ConfigureServices()
		{
			IServiceCollection services = new ServiceCollection();

			var section = _configuration.GetSection("AppSettings");
			var connectionString = _configuration.GetConnectionString("DefaultConnection");

			services.Configure<ApplicationSettings>(section);

			services.AddLogging(configure => configure.AddConsole());
			services.AddDbContext<AppDataContext>(options => 
				options.UseLazyLoadingProxies()
				.UseSqlServer(connectionString, x => x.MigrationsAssembly("ChatBotPrime.Infra.Data.EF"))
			);


			services.AddSingleton<IRepository, EfGenericRepo>();

			ConfigureChatServices(services, section);
			ConfigureChatMessages(services);

			services.AddSingleton<ChatHandlerService>();
			services.AddSingleton<SignalRService>();

			return services;
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
