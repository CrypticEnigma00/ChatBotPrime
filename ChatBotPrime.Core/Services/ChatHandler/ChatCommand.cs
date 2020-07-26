using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Message = ChatBotPrime.Core.Events.EventArguments.ChatMessage;

namespace ChatBotPrime.Core.Services.CommandHandler
{
	public abstract class ChatCommand : IChatCommand
	{
		
		public abstract string CommandText { get; }
		public abstract string Response(IChatService service, Message chatMessage);
		public abstract TimeSpan Cooldown { get; }
		public DateTime LastRun { get; set; } = DateTime.UtcNow;
		public bool IsAllowedToRun => (DateTime.UtcNow - LastRun) >= Cooldown;

		public bool IsMatch(string command)
		{

			return command.Equals(CommandText, StringComparison.InvariantCultureIgnoreCase);
		}

		protected void SetLastRun()
		{
			LastRun = DateTime.UtcNow;
		}

		protected bool CanRun()
		{
			if (Cooldown == TimeSpan.Zero)
				return true;

			return DateTime.UtcNow - LastRun <= Cooldown;
		}

		protected string GetTimeToRun()
		{
			var time = (Cooldown - (DateTime.UtcNow - LastRun));
			if (time.Minutes > 0)
			{
				return $"{time.Minutes}M{time.Seconds}S";
			}
			else
			{
				return $"{time.Seconds}S";
			}
		}

	}
}
