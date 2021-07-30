namespace BloodyBotSystem.BotEvents
{
    using System;

    public class BotStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Time that the bot was started at
        /// </summary>
        public DateTime StartedAt { get; set; }
    }
}