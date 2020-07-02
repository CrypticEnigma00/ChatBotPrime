using ChatBotPrime.Core.Configuration;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Infra.Chat.Twitch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ChatBotPrime.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {


            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", true, true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            IServiceCollection services = new ServiceCollection();

            var section = config.GetSection("AppSettings");

            services.Configure<ApplicationSettings>(section);


            services.AddSingleton<IChatService, TwitchChatService>();

            var sp = services.BuildServiceProvider();

            IChatService tcs = sp.GetService<IChatService>();


            Console.ReadLine();

            tcs.Disconnect();

        }
    }
}
