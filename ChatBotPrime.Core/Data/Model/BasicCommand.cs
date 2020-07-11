using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Data.Model
{
	public class BasicCommand	: DataEntity
	{
		public string MessageText { get; set; }
		public string Response { get; set; }
		public TimeSpan Cooldown { get; set; } = TimeSpan.Zero;
		public List<CommandAlias> Aliases { get; set; }

	}
}
