using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Specifications;
using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Interfaces.Stream;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotPrime.Infra.ChatHander
{
	public class ChatHandlerService
	{
		private IEnumerable<IChatService> _chatServices;
		private List<IChatCommand> _commands;
		private List<IChatMessage> _messages;


		public ChatHandlerService(IEnumerable<IChatService> chatServices, IEnumerable<IChatCommand> commands, IEnumerable<IChatMessage> messages, IRepository repository)
		{
			_chatServices = chatServices;

			_commands = commands.ToList();
			_messages = messages.ToList();
			
			AddEventHandlersToChatServices();

			_commands.AddRange(repository.List(BasicCommandPolicy.All()).AsEnumerable());
			_messages.AddRange(repository.List(BasicMessagePolicy.All()).AsEnumerable());
		}

		private void AddEventHandlersToChatServices()
		{
			foreach (IChatService svc in _chatServices)
			{
				svc.OnCommandReceived += CommandHander;
				svc.OnMessageReceived += MessageHandler;
			}
		}

		private void CommandHander(object sender, ChatCommandReceivedEventArgs e)
		{
			if (sender is IChatService service)
			{
				var command = GetCommand(e.CommandText);

				if (command is IStreamCommand)
				{
					if (!(service is IStreamService))
					{
						service.SendMessage("Command is for use in stream based Service and cannot be run from a chat only Service");
					}
				}

				service.SendMessage(command.Response(service));
			}
		}

		private IChatCommand GetCommand(string commandText)
		{
			return _commands.First(c => c.IsMatch(commandText));
		}


		private void MessageHandler(object sender, ChatMessageReceivedEventArgs e)
		{
			if (sender is IChatService service)
			{
				var message = GetMessage(e.ChatMessage.Message);

				if (!(message is null))
				{
					service.SendMessage(message.Response(e));
				}
			}
		}

		private IChatMessage GetMessage(string messageText)
		{
			return _messages.FirstOrDefault(m => m.IsMatch(messageText));
		}
	}
}
