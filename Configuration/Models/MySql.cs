namespace TicketBot.Models
{
   public class MySql
    {
        /// <summary>
        /// IP Address of the Host
        /// </summary>
        public string MySqlHost { get; set; }

        /// <summary>
        /// The User the bot will log in as
        /// </summary>
        public string MySqlUser { get; set; }

        /// <summary>
        /// The Password Used to log in
        /// </summary>
        public string MySqlPass { get; set; }

        /// <summary>
        /// The Database the bot will access
        /// </summary>
        public string MySqlDb { get; set; }

        /// <summary>
        /// The SQL Server Port
        /// </summary>
        public int MySqlPort { get; set; }
    }
}
