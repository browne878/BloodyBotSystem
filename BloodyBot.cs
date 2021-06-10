namespace BloodyBotSystem
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BloodyBotSystem.Configuration.Models;
    using BloodyBotSystem.Configuration.Services;
    using BloodyBotSystem.Global_Events;
    using DSharpPlus;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.SlashCommands;


    public class BloodyBot
    {
        public DiscordClient Bot { get; set; }

        private DatabaseManager Database { get; set; }

        private FileService FileService { get; set; }
        private ArkCommandManager RconManager { get; set; }

        public Config Config { get; private set; }

        private SlashCommandsExtension SlashCommandsExtension { get; set; }

        /// <summary>
        /// Fired when the Bot is Started
        /// </summary>
        public event EventHandler<BotStartedEventArgs> BotStarted;

        /// <summary>
        /// Initializes Properties of the Bot
        /// </summary>
        public void InitialiseBot()
        {
            Database = new DatabaseManager(this);
            FileService = new FileService();
            RconManager = new ArkCommandManager(this);

            this.Bot = new DiscordClient(new DiscordConfiguration()
            {
                AutoReconnect = true,
                Token = Config.DiscordOptions.Token,
                TokenType = TokenType.Bot
            });
        }

        /// <summary>
        /// Enables DSharp Commands Next
        /// </summary>
        /// <param name="_services"></param>
        public void UseCommandsNext(IServiceProvider _services)
        {
            this.Bot.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new[] { Config.DiscordOptions.Prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                Services = _services,
                EnableDefaultHelp = false,
                CaseSensitive = false,
                IgnoreExtraArguments = true
            });
        }

        /// <summary>
        /// Enables Slash Commands
        /// </summary>
        /// <param name="_services"></param>
        public void UseSlashCommands(IServiceProvider _services)
        {
            SlashCommandsExtension = this.Bot.UseSlashCommands(new SlashCommandsConfiguration { Services = _services });
        }

        /// <summary>
        /// Initialise the config with the default style.
        /// </summary>
        /// <param name="_path">Path of the config file</param>
        public void InitialiseConfigs(string _path)
        {
            Config = FileService.GetConfig(_path);
        }


        /// <summary>
        /// Registers all the commands
        /// </summary>
        /// <param name="_types">Array of Command Types to be Registered</param>
        public void InitialiseSlashCommands(Type[] _types)
        {
            foreach (Type type in _types)
            {
                SlashCommandsExtension.RegisterCommands(type, 764600703624282153);
            }
        }

        /// <summary>
        /// Turns the bot on.
        /// </summary>
        /// <returns>Infinite Timeout</returns>
        public async Task ConnectAsync()
        {
            await Bot.ConnectAsync();
            await Task.Delay(Timeout.Infinite);

            BotStarted?.Invoke(this, new BotStartedEventArgs()
            {
                StartedAt = DateTime.Now
            });
        }

        /// <summary>
        /// Sends an RCON Command to a server from the config
        /// </summary>
        /// <param name="_command">Command to be executed via RCON</param>
        /// <param name="_serverName">Name of the target server from the config</param>
        /// <returns>String - Server Response</returns>
        public async Task<string> ExecuteRconCommand(string _command, string _serverName) => await RconManager.RconSendCommand(_command, _serverName);

        /// <summary>
        /// Queries a Database from the config
        /// </summary>
        /// <param name="_queryCommand">MySQL Command string</param>
        /// <param name="_dbIndex">Index of the database in the config</param>
        public void VoidQuery(string _queryCommand, int _dbIndex) => Database.VoidQuery(_queryCommand, _dbIndex);

        /// <summary>
        /// Queries the Database and returns the result.
        /// </summary>
        /// <param name="_queryCommand">MySQL Command to be sent.</param>
        /// <param name="_dbIndex">Index of the database in the config. Default 0.</param>
        /// <returns>Object</returns>
        public object ObjectQuery(string _queryCommand, int _dbIndex) => Database.ObjectQuery(_queryCommand, _dbIndex);
    }
}
