using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Data.Model
{
	public class BasicMessage : DataEntity
	{
		public string MessageTest { get; set; }
		public string Response { get; set; }
		public List<MessageAlias> Aliases { get; set; }
	}
}
