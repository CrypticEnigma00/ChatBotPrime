using ChatBotPrime.Core.Interfaces.Chat;
using ChatBotPrime.Core.Interfaces.Stream;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler
{
	public abstract class StreamCommand : ChatCommand, IStreamCommand
	{
	}
}
