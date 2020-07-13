using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatBotPrime.Core.Data.Model
{
	public class BasicMessage : DataEntity, IChatMessage
	{
		public BasicMessage()
		{ }
		public BasicMessage(string messageText, string response, List<MessageAlias> aliases = null)
		{
			MessageText = messageText;
			Response = response;
			Aliases = aliases;
		}

		public string Response { get; private set; }
		public virtual ICollection<MessageAlias> Aliases { get; set; }

		public string MessageText {get; private set;}

		public bool IsMatch(string messageText)
		{

			return messageText.Contains(MessageText,StringComparison.InvariantCultureIgnoreCase) || Aliases.Where(a => messageText.Contains(a.Word,StringComparison.InvariantCultureIgnoreCase)).Any();
		}

		string IChatMessage.Response(ChatMessageReceivedEventArgs e)
		{
			return Response;
		}
	}
}
