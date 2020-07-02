namespace ChatBotPrime.Core.Interfaces.Chat
{
	public interface IChatService
	{
		bool _connected { get; }

		void Connect();
		void Disconnect();
		void JoinChannel(string channel);
		void SendMessage(string message);
		void SendMessage(string channel, string message);
	}
}