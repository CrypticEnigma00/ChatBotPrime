using ChatBotPrime.Core.Interfaces.Stream;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler.Commands
{
	public class UpTimeCommand : StreamCommand
	{
		public override string CommandText => "Uptime";

		public override TimeSpan Cooldown => TimeSpan.FromSeconds(300);

		public override string Response(IStreamService streamService)
		{
			var upTime = streamService.UpTime();

			if (upTime.ToLower() == "offline")
			{
				return $"The Stream is currently offline and has no uptime.";
			}

			return $"The Stream has been running for { upTime }";
		}
	}
}
