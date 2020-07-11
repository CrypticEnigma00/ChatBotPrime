﻿using ChatBotPrime.Core.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ChatBotPrime.Infra.Data.EF
{
	public class AppDataContext : DbContext
	{
		public DbSet<BasicCommand> BasicCommands { get; set; }
		public DbSet<BasicMessage> BasicMessages { get; set; }

		public AppDataContext(DbContextOptions options) : base(options)
		{
			
		}


	}

	public class AppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
	{
		public AppDataContext CreateDbContext(string[] args)
		{
			// Build config
			IConfiguration config = new ConfigurationBuilder()
				.AddUserSecrets<AppDataContext>()
				.Build();

			var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
			var connectionString = config.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("ChatBotPrime.Infra.Data.EF"));
			return new AppDataContext(optionsBuilder.Options);
		}
	}
}
