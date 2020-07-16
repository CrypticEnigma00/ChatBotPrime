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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace ChatBotPrime.ConsoleApp
{
	class Program
	{
		private static IConfiguration _configuration { get; set; }

		static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services
					.ConfigureApplicationServices(hostContext.Configuration)
					.AddHostedService<ChatBotPrimeService>();
			});
	}
}
