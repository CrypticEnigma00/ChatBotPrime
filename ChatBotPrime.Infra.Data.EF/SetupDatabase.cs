using ChatBotPrime.Core.Data;
using ChatBotPrime.Core.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatBotPrime.Infra.Data.EF
{
	public static class SetupDatabase
	{
		public static void Configure(AppDataContext appDataContext, IRepository repository)
		{
			EnsureDatabase(appDataContext);
			EnsureInitialData(repository);
		}

		//public static IRepository SetupRepository(string connectionString)
		//{
		//	DbContextOptions<AppDataContext> options = new DbContextOptionsBuilder<AppDataContext>()
		//	   .UseSqlServer(connectionString)
		//	   .Options;

		//	AppDataContext appDataContext = new AppDataContext(options);

		//	EnsureDatabase(appDataContext);
		//	var repository = new EfGenericRepo(appDataContext);
		//	EnsureInitialData(repository);

		//	return repository;
		//}
		private static void EnsureDatabase(AppDataContext appDataContext)
		{
			appDataContext.Database.Migrate();
		}

		private static void EnsureInitialData(IRepository repository)
		{
			if (!repository.List<BasicCommand>().Any())
			{
				//TODO:Database: Implament Base Commands to be added 
			}

			if (!repository.List<BasicMessage>().Any())
			{
				//TODO:Database: Implament Base Messages to be added 
			}
		}
	}
}
