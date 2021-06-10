namespace BloodyBotSystem.Configuration.Models
{
    using System.Collections.Generic;
    using TicketBot.Models;

    public class Config
    {
        /// <summary>
        /// Bot Options
        /// </summary>
        public DiscordOptions DiscordOptions { get; set; }

        /// <summary>
        /// List of Ingame Servers
        /// </summary>
        public List<Servers> Servers { get; set; }

        /// <summary>
        /// List of MySQL Servers
        /// </summary>
        public List<MySql> MySql { get; set; }
    }
}
