using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Interfaces.Stream;
using ChatBotPrime.Core.Services.CommandHandler;
using ChatBotPrime.Core.Services.CommandHandler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotPrime.Infra.CommandHander
{
	public class CommandHandlerService
	{
		private IEnumerable<IChatService> _chatServices;
		private List<IChatCommand> _commands;
		

		public CommandHandlerService(IEnumerable<IChatService>  chatServices)
		{
			_chatServices = chatServices;

			AddEventHandlersToChatServices();
			GetCommandList();
		}

		private void AddEventHandlersToChatServices()
		{
			foreach (IChatService svc in _chatServices)
			{
				svc.OnCommandReceived += CommandHander;
			}
		}

		private void GetCommandList()
		{
			_commands = new List<IChatCommand>();
			_commands.Add(new PingCommand());
			_commands.Add(new UpTimeCommand());
		}

		private void CommandHander(object sender, ChatCommandReceivedEventArgs e)
		{
			var command = GetCommand(e.CommandText);

			if (command is IStreamCommand)
			{
				if (!(sender is IStreamService))
				{
					((IChatService)sender).SendMessage("Command is for use in stream Services and cannot be run from a chat only Service");
					return;
				}
				((IStreamService)sender).SendMessage(((IStreamCommand)command).Response((IStreamService)sender));
				return;
			};

			((IChatService)sender).SendMessage(command.Response());

		}

		private IChatCommand GetCommand(string commandText)
		{
			return _commands.Where(c => c.CommandText.ToLower() == commandText.ToLower()).FirstOrDefault();
		}
	}
}
