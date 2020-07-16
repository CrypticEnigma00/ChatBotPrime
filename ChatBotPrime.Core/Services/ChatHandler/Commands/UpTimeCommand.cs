﻿using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Interfaces.Stream;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Message = ChatBotPrime.Core.Events.EventArguments.ChatMessage;

namespace ChatBotPrime.Core.Services.CommandHandler.Commands
{
	public class UpTimeCommand : StreamCommand
	{
		public override string CommandText => "Uptime";
		public override TimeSpan Cooldown => TimeSpan.FromSeconds(300);

		public override string Response(IChatService streamService, Message chatMessage)
		{
			var service = (IStreamService)streamService;

			var upTime = service.UpTime();

			if (upTime.ToLower() == "offline")
			{
				return $"The Stream is currently offline and has no uptime.";
			}

			return $"The Stream has been running for { upTime }";
		}


	}
}
