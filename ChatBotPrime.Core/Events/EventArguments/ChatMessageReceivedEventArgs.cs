using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Events.EventArguments
{
	public class ChatMessageReceivedEventArgs
	{
		public ChatMessageReceivedEventArgs(ChatMessage chatMessage)
		{
			ChatMessage = chatMessage;
		}

		public ChatMessage ChatMessage { get; }
	}
}
