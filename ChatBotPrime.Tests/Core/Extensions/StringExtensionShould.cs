using System.Linq;
using ChatBotPrime.Core.Extensions;
using Xunit;

namespace ChatBotPrime.Tests
{
	public class StringExtensionShould
	{

		[Theory]
		[InlineData("word")]
		[InlineData("Word")]
		[InlineData("this")]
		[InlineData("thiS")]
		public void ReturnTrue_GivenWordContainedInString(string word)
		{
			// Arrange
			var testString = "this must contain the test word";

			//Act
			var result = testString.Contains(word, System.StringComparison.InvariantCultureIgnoreCase);
			//Assert
			Assert.True(result);
		}

		[Theory]
		[InlineData("easy")]
		[InlineData("Help")]
		[InlineData("primary")]
		[InlineData("primarY")]
		public void ReturnFalse_GivenWordNotContainedInString(string word)
		{
			// Arrange
			var testString = "this must contain the test word";

			//Act
			var result = testString.Contains(word, System.StringComparison.InvariantCultureIgnoreCase);
			//Assert

			Assert.False(result);
		}

		[Theory]
		[InlineData("[me]")]
		[InlineData("[UserDisplayName]")]
		public void ReturnToken_GivenStringWithToken(string replacer)
		{
			//Arrange
			string testString = $"please replace {replacer}";
			//Act
			var tokens = testString.FindTokens();
			//Assert
			Assert.Contains(replacer, tokens.FirstOrDefault());
		}
	}
}
