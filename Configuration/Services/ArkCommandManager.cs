namespace BloodyBotSystem.Configuration.Services
{
    using System.Threading.Tasks;
    using BloodyBotSystem;
    using RconSharp;

    public class ArkCommandManager
    {
        private readonly BloodyBot bot;
        private RconClient rconClient;
        public ArkCommandManager(BloodyBot _bot)
        {
            this.bot = _bot;
        }

        private async Task<bool> OpenRcon(int _serverId)
        {
            rconClient = RconClient.Create(bot.Config.Servers[_serverId].RconIP, bot.Config.Servers[_serverId].RconPort);
            await rconClient.ConnectAsync();
            bool isAuth = await rconClient.AuthenticateAsync(bot.Config.Servers[_serverId].RconPass);
            return isAuth;
        }

        private async Task<string> RconCommand(string _command, int _serverId)
        {
            //serverid = check which server in array for execute
            //openRcon check if connection is valid
            //command like addpoints etc
            if (await OpenRcon(_serverId))
            {
                string response = await rconClient.ExecuteCommandAsync(_command);
                rconClient.Disconnect();

                //response is rcon respond like if you use !rcon island etc
                return response;

            }
            else
            {
                rconClient.Disconnect();
                return "Server is offline.";

            }
        }

        protected internal async Task<string> RconSendCommand(string _command, string _serverName)
        {
            int serverId = -1;
            for (int i = 0; i < bot.Config.Servers.Count; i++)
            {
                if (bot.Config.Servers[i].ServerName == _serverName)
                {
                    serverId = i;
                }
            }
            string result = await RconCommand(_command, serverId);
            return result;
        }
    }
}
