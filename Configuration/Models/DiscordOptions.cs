namespace BloodyBotSystem.Configuration.Models
{
   public class DiscordOptions
    {
        /// <summary>
        /// Discord Bot Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Prefix for the commands
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// ID for the Admin Role
        /// </summary>
        public ulong AdminId { get; set; }

        /// <summary>
        /// ID for the PVE Admin Role
        /// </summary>
        public ulong PveAdminId { get; set; }

        /// <summary>
        /// ID for the Community Support Role
        /// </summary>
        public ulong CommSupportId { get; set; }
    }
}
