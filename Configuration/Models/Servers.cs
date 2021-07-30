namespace BloodyBotSystem.Configuration.Models
{
    public class Servers
    {
        /// <summary>
        /// The Cluster the Server is on
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        /// Unique Server Name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Rcon IP Address
        /// </summary>
        public string RconIp { get; set; }

        /// <summary>
        /// Rcon Password for that server
        /// </summary>
        public string RconPass { get; set; }

        /// <summary>
        /// The Rcon Port
        /// </summary>
        public int RconPort { get; set; }
    }
}
