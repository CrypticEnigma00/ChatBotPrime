using ChatBotPrime.Core.Interfaces.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		string IChatCommand.Response(IChatService service)
		{
			return Response;
		}
	}
}
