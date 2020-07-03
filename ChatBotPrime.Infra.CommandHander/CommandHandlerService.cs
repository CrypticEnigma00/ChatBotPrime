using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Services.CommandHandler;
using ChatBotPrime.Core.Services.CommandHandler.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace ChatBotPrime.Infra.CommandHander
{
	public class CommandHandlerService
	{
		private IChatService _chatService;
		private List<IChatCommand> _commands;
		

		public CommandHandlerService(IChatService chatService)
		{
			_chatService = chatService;

			_chatService.OnCommandReceived += CommandHander;

			GetCommandList();
		}

		private void GetCommandList()
		{
			_commands = new List<IChatCommand>();
			_commands.Add(new PingCommand());
			_commands.Add(new UpTimeCommand());
		}

		private void CommandHander(object sender, ChatCommandReceivedEventArgs e)
		{
			IChatCommand command = GetCommand(e.CommandText);

			_chatService.SendMessage(command.Response);
		}

		private IChatCommand GetCommand(string commandText)
		{
			return _commands.Where(c => c.CommandText.ToLower() == commandText.ToLower()).FirstOrDefault();
		}
	}
}
