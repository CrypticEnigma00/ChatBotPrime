using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Services.CommandHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using Message = ChatBotPrime.Core.Events.EventArguments.ChatMessage;
using System.Text;
using ChatBotPrime.Core.Extensions;

namespace ChatBotPrime.Core.Data.Model
{
	public class BasicCommand	: DataEntity , IChatCommand
	{
		public BasicCommand()
		{ }
		public BasicCommand(string commandText, string response, bool isAllowedToRun = true  )
		{
			CommandText = commandText;
			Response = response;
			IsAllowedToRun = IsAllowedToRun;
		}
			
		public TimeSpan Cooldown { get; set; } = TimeSpan.Zero;
		public virtual ICollection<CommandAlias> Aliases { get; set; }

		public string CommandText { get; private set; }

		public bool IsAllowedToRun { get; private set; }

		public DateTime LastRun { get; set; }

		public bool IsMatch(string command)
		{
			return command.Equals(CommandText, StringComparison.InvariantCultureIgnoreCase) || Aliases.Where(a => a.Word.Equals(command,StringComparison.InvariantCultureIgnoreCase)).Any();
		}

		public string Response { get; private set; }

		string IChatCommand.Response(IChatService service,Message chatMessage)
		{
			IEnumerable<string> findTokens = Response.FindTokens();
			string textToSend = ReplaceTokens(Response, findTokens, chatMessage);
			return textToSend;
		}

		private string ReplaceTokens(string textToSend, IEnumerable<string> tokens, Message chatMessage)
		{
			string newText = textToSend;
			var replaceableTokens = TokenReplacer.ListAll.Where(x => tokens.Contains(x.ReplacementToken));
			foreach (var rt in replaceableTokens)
			{
				newText = rt.ReplaceValues(newText, chatMessage);
			}

			return newText;
		}
	}
}
