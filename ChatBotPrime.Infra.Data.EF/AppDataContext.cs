using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ChatBotPrime.Infra.Data.EF
{
	public class AppDataContext : DbContext
	{
		public AppDataContext(DbContextOptions options) : base(options)
		{

		}
	}
}
