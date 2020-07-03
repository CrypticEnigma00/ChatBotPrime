

using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using System;

namespace ChatBotPrime.Infra.Chat.Discord
{
	public class DiscordChatService : IChatService
	{
		public bool _connected => throw new NotImplementedException();

		public event EventHandler<ChatCommandReceivedEventArgs> OnCommandReceived;
		public event EventHandler<ChatMessageReceivedEventArgs> OnMessageReceived;

		public void Connect()
		{
			throw new NotImplementedException();
		}

		public void Disconnect()
		{
			throw new NotImplementedException();
		}

		public void JoinChannel(string channel)
		{
			throw new NotImplementedException();
		}

		public void SendMessage(string message)
		{
			throw new NotImplementedException();
		}

		public void SendMessage(string channel, string message)
		{
			throw new NotImplementedException();
		}
	}
}
