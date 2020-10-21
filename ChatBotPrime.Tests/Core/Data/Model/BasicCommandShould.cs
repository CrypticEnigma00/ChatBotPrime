using ChatBotPrime.Core.Data.Model;
using ChatBotPrime.Core.Events.EventArguments;
using ChatBotPrime.Core.Interfaces.Chat;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ChatBotPrime.Tests.Core.Data.Model
{
	public class BasicCommandShould
	{
		private IChatCommand GenerateBasicCommand(bool isEnabled = true)
		{
			var bc = new BasicCommand("test", "response [UserDisplayName]", isEnabled) {
				Cooldown = TimeSpan.FromMinutes(5),


			};

			var alias1 = new CommandAlias()
			{
				command = bc,
				Word = "alias1"
			};

			var alias2 = new CommandAlias()
			{
				command = bc,
				Word = "alias2"
			};

			var la = new List<CommandAlias>();

			la.Add(alias1);
			la.Add(alias2);

			bc.Aliases = la;

			return bc;
		}

		private ChatCommand GenerateChatMessage()
		{
			 return new ChatCommand(new List<string> { "a" },"a",'!', "test", new ChatMessage(null, false, false, false, false, false, 0, null, null, 0, false, null, "TokenReplacement"));
		}

		[Fact]
		public void ReturnsTrueIsMatch_GivenMatchingCommandText()
		{
			//Arange
			var bc = GenerateBasicCommand();
			//Act
			var result = bc.IsMatch("test");
			//Assert
			Assert.True(result);

		}

		[Fact]
		public void ReturnsFalseIsMatch_GivenNonMatchingCommandText()
		{
			//Arange
			var bc = GenerateBasicCommand();
			//Act
			var result = bc.IsMatch("failure");
			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ReturnsFalseIsMatch_GivenDisabledCommand()
		{
			//Arange
			var bc = GenerateBasicCommand(false);
			//Act
			var result = bc.IsMatch("test");
			//Assert
			Assert.False(result);
		}

		[Fact]
		public void ReturnsStringFromRsponse_GivenTimeGraterThanFvieMinCooldown()
		{
			//Arange
			Mock<IChatService> cs = new Mock<IChatService>();
			var message = GenerateChatMessage();
			var bc = GenerateBasicCommand();
			bc.LastRun = DateTime.UtcNow - TimeSpan.FromMinutes(6);
			//Act
			var result = bc.Response(cs.Object,message);
			//Assert
			Assert.IsAssignableFrom<string>(result);
			Assert.Contains("response", result);
		}	  
		
		[Fact]
		public void ReturnsStringFromRsponse_GivenZeroCooldown()
		{
			//Arange
			Mock<IChatService> cs = new Mock<IChatService>();
			var message = GenerateChatMessage();
			var bc = GenerateBasicCommand();
			bc.Cooldown = TimeSpan.Zero;
			//Act
			var result = bc.Response(cs.Object,message);
			//Assert
			Assert.IsAssignableFrom<string>(result);
			Assert.Contains("response", result);
		}

		[Fact]
		public void ReturnsStringFromRsponse_GivenTimeSmallerThenCooldown()
		{
			//Arange
			Mock<IChatService> cs = new Mock<IChatService>();
			var message = GenerateChatMessage();
			var bc = GenerateBasicCommand();
			//Act
			var result = bc.Response(cs.Object,message);
			//Assert
			Assert.IsAssignableFrom<string>(result);
			Assert.Contains("cooldown", result);
			Assert.Matches(@"\d+M\d+S", result);
		}

		[Fact]
		public void ReturnsStringFromRsponse_GivenTimeSmallerThenCooldownAndLessThan60SFromCoolDown()
		{
			//Arange
			Mock<IChatService> cs = new Mock<IChatService>();
			var message = GenerateChatMessage();
			var bc = GenerateBasicCommand();
			bc.LastRun = DateTime.UtcNow - TimeSpan.FromSeconds(250);
			//Act
			var result = bc.Response(cs.Object, message);
			//Assert
			Assert.IsAssignableFrom<string>(result);
			Assert.Contains("cooldown", result);
			Assert.Matches(@"\d+S", result);
			
		}


	}
}
