using ChatBotPrime.Core.Configuration;
using ChatBotPrime.Core.Interfaces.Chat;
using Microsoft.Extensions.Options;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace ChatBotPrime.Infra.Chat.Twitch
{
	public class TwitchChatService : IChatService
	{
		private TwitchClient _client;
		private readonly TwitchSettings _settings;
		private JoinedChannel _channel;
		public bool _connected => _client.IsConnected;

		public TwitchChatService(IOptions<ApplicationSettings> applicationSettings)
		{
			_settings = applicationSettings.Value.TwitchSettings;

			var creds = CreateCredentials();
			CreateClient();
			if (_client != null)
			{
				Initialize(creds);
				ConfigureHandlers();
				_client.AddChatCommandIdentifier(_settings.CommandIdentifier);
				_client.Connect();
			}

		}

		public void SendMessage(string message)
		{
		   SendMessage(_channel.Channel, message);
		}

		public void SendMessage(string channel, string message)
		{
			_client.SendMessage(channel, message);
		}

		public void Connect()
		{
			if (_client != null)
			{
				_client.Connect();
			}
		}

		public void Disconnect()
		{
			_client.Disconnect();
		}

		public void JoinChannel(string channel)
		{
			_client.JoinChannel(channel);
			Console.WriteLine($"Joined channel : {channel}");

		}

		private ConnectionCredentials CreateCredentials()
		{
			return new ConnectionCredentials(_settings.Username, _settings.Token);
		}

		private void CreateClient()
		{
			var clientOptions = new ClientOptions
			{
				MessagesAllowedInPeriod = 750,
				ThrottlingPeriod = TimeSpan.FromSeconds(30)
			};
			WebSocketClient customClient = new WebSocketClient(clientOptions);
			_client = new TwitchClient(customClient);

		}

		private void Initialize(ConnectionCredentials creds)
		{
			_client.Initialize(creds);
		}



		private void ConfigureHandlers()
		{
			_client.OnMessageReceived += OnMessageReceived;
			_client.OnConnected += OnConnected;
			_client.OnChatCommandReceived += OnCommandReceived;
		}

		private void OnConnected(object sender, OnConnectedArgs args)
		{
			Console.WriteLine($"Connection To Twitch Started.");
			JoinChannel(_settings.Channel);
			_channel = _client.GetJoinedChannel(_settings.Channel);
		}


		private void OnCommandReceived(object sender, OnChatCommandReceivedArgs args)
		{
			Console.WriteLine($"Command Received from Chat : {args.Command.CommandText}  aarguments : {args.Command.ArgumentsAsString}");
		}

		private void OnMessageReceived(object sender, OnMessageReceivedArgs args)
		{
			string msg = args.ChatMessage.Message;

			if (!msg.StartsWith(_settings.CommandIdentifier.ToString()))
			{
				Console.WriteLine("Message Received from Chat");
			}
		}
	}
}