using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler.Commands
{
	public class PingCommand : ChatCommand
	{
		public override string CommandText => "Ping";

		public override string Response => "Pong";

		public override TimeSpan Cooldown => TimeSpan.FromSeconds(0);
	}
}
