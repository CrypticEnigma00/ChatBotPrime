using Xunit;
using ChatBotPrime.Core.Services.CommandHandler;
using ChatMessage = ChatBotPrime.Core.Events.EventArguments.ChatMessage;

namespace ChatBotPrime.Tests
{
	public class TokenReplacerShould
	{
		[Theory]
		[InlineData("CrypticEnigma00")]
		[InlineData("[CrypticEnigma00]")]
		[InlineData("//[CrypticEnigma00]")]
		public void ReturnString_GivenUserDisplayNameToken(string replacement)
		{
			//Arrange
			var message = new ChatMessage(null,false,false,false, false,false, 0, null, null, 0,  false,  null, replacement);
			var tr = TokenReplacer.UserDisplayName;
			string testString = $"please replace [UserDisplayName]";


			string expectedString = $"please replace {replacement}";

			//Act
			var result = tr.ReplaceValues(testString, message);
			//Assert
			Assert.Equal(expectedString, result);
		}
	}
}
