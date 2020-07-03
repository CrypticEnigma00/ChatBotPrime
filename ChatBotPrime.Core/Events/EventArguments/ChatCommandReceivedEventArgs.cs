using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Events.EventArguments
{
	public class ChatCommandReceivedEventArgs	: EventArgs
	{
		public ChatCommandReceivedEventArgs(List<string> argumentsAsList, char commandIdentifier, string commandText, ChatMessage chatMessage)
		{
			ArgumentsAsList = argumentsAsList;
			CommandIdentifier = commandIdentifier;
			CommandText = commandText;
			ChatMessage = chatMessage;
		}

		public List<string> ArgumentsAsList { get; private set; }
		public string ArgumentsAsString => ArgumentsAsList.ToString();
		public char CommandIdentifier { get; private set; }
		public string CommandText { get; private set; }


		public ChatMessage ChatMessage { get; private set; }
	}
}
