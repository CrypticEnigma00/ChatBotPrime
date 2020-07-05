using ChatBotPrime.Core.Interfaces.Stream;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Services.CommandHandler
{
	public abstract class StreamCommand : ChatCommand, IStreamCommand
	{

		public abstract string Response(IStreamService streamService);

		public override string Response()
		{
			throw new NotImplementedException();
		}

	}
}
