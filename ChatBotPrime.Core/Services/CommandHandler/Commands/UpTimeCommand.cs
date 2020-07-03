using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler.Commands
{
	public class UpTimeCommand : ChatCommand
	{

		public override string CommandText => "Uptime";

		public override string Response => $"The Stream has been running for { GetUptime() }";

		public override TimeSpan Cooldown => TimeSpan.FromSeconds(300);

		

		private string GetUptime()
		{
			//TODO:Get up time and return it.
			return "";
		}
	}
}
