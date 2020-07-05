using System;

namespace ChatBotPrime.Core.Interfaces.Chat
{
	public interface IChatCommand
	{
		string CommandText { get; }
		TimeSpan Cooldown { get; }
		bool IsAllowedToRun { get; }
		DateTime LastRun { get; set; }
		string Response();
	}
}