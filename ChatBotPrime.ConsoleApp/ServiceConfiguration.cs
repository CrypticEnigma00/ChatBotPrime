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
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChatBotPrime.ConsoleApp
{
	public static class ServiceConfiguration
	{
		private static IConfigurationRoot SetupConfiguration(string[] args)
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", true, true)
				.AddUserSecrets<Program>()
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();
		}
		public static  IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration,string[] args)
		{
			var config = SetupConfiguration(args);

			var section = config.GetSection("AppSettings");
			var connectionString = config.GetConnectionString("DefaultConnection");

			services.Configure<ApplicationSettings>(section);

			services.AddLogging(configure => configure.AddConsole());
			services.AddDbContext<AppDataContext>( options =>
				options.UseLazyLoadingProxies()
				.UseSqlServer(connectionString, x => x.MigrationsAssembly("ChatBotPrime.Infra.Data.EF")),
				ServiceLifetime.Singleton
			);


			services.AddSingleton<IRepository, EfGenericRepo>();

			ConfigureChatServices(services, section);
			ConfigureChatMessages(services);

			services.AddSingleton<ChatHandlerService>();
			services.AddSingleton<SignalRService>();

			return services;
		}

		private static void ConfigureChatServices(IServiceCollection services, IConfigurationSection section)
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
				.Select(x => x);


			foreach (var cmd in commands)
			{
				services.AddSingleton(sp => ActivatorUtilities.CreateInstance(sp, cmd));
			}
		}

		private static void ConfigureChatMessages(IServiceCollection services)
		{
			var messages = Assembly.GetAssembly(typeof(IChatMessage)).GetTypes()
				.Where(x => x.Namespace == "ChatBotPrime.Core.Services.CommandHandler.Messages")
				.Where(x => x.IsClass)
				.Select(x => x);

			foreach (var msg in messages)
			{
				services.AddSingleton(sp => ActivatorUtilities.CreateInstance(sp, msg));
			}

		}
	}
}
