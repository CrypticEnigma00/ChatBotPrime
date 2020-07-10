using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using System;

namespace ChatBotPrime.Core.Services.CommandHandler
{
	public abstract class ChatMessage : IChatMessage
	{
		public abstract string MessageText { get; }

		public abstract bool IsMatch(string messageText);
		
		public abstract string Response(ChatMessageReceivedEventArgs e);
	}
}
