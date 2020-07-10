using ChatBotPrime.Core.Interfaces.Chat;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler
{
	public abstract class ChatCommand : IChatCommand
	{
		
		public abstract string CommandText { get; }
		public abstract string Response(IChatService service);
		public abstract TimeSpan Cooldown { get; }
		public DateTime LastRun { get; set; }
		public bool IsAllowedToRun => (DateTime.UtcNow - LastRun) >= Cooldown;

		public bool IsMatch(string command)
		{

			return command.Equals(CommandText, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
