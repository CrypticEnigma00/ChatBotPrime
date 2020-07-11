using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotPrime.Core.Data.Model
{
	public class MessageAlias  : DataEntity
	{
		public BasicMessage Message { get; set; }
		public string Response { get; set; }
	}
}
