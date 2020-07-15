using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Infra.ChatHander;
using ChatBotPrime.Infra.Data.EF;
using ChatBotPrime.Infra.SignalRCommunication;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotPrime.ConsoleApp
{
	public class ChatBotPrimeService : IHostedService
	{
		private IRepository _repository;
		private AppDataContext _appDataContext;
		private IEnumerable<IChatService> _chatServices;
		private ChatHandlerService _chatHandlerService;
		private SignalRService _signalRService;

		public ChatBotPrimeService(IRepository repository, AppDataContext appDataContext, IEnumerable<IChatService> chatServices, ChatHandlerService chatHandlerService, SignalRService signalRService)
		{
			_repository = repository;
			_appDataContext = appDataContext;
			_chatServices = chatServices;
			_chatHandlerService = chatHandlerService;
			_signalRService = signalRService;

			ConfigureDB();
		}

		private void ConfigureDB()
		{
			SetupDatabase.Configure(_appDataContext,_repository);
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			foreach (var svc in _chatServices)
			{
				svc.Connect();
			}
			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			foreach (var svc in _chatServices)
			{
				svc.Disconnect();
			}
			return Task.CompletedTask;
		}
	}
}
