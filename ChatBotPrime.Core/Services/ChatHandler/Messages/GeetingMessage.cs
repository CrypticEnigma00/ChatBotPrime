using ChatBotPrime.Core.Events.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler.Messages
{
	public class GeetingMessage : ChatMessage
	{
		public override string MessageText => "Hi , Hello, Hey";

		public override bool IsMatch(string messageText)
		{
			var greeting = MessageText.Split(',');
		
			return greeting.Any(g => MessageText.StartsWith(g));
		}

		public override string Response(ChatMessageReceivedEventArgs e)
		{
			return $"Welcome {e.ChatMessage.Username}, Glad you could join us.";
		}
	}
}
