using ChatBotPrime.Core.Interfaces.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Infra.CommandHander
{
	public class CommandHandlerService
	{
		private IChatService _chatService;

		public CommandHandlerService(IChatService chatService)
		{
			_chatService = chatService;
		}

		private void test()
		{
			
		}
	}
}
