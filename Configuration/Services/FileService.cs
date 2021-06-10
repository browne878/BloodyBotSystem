namespace BloodyBotSystem.Configuration.Services
{
    using System;
    using System.IO;
    using BloodyBotSystem.Configuration.Models;
    using Newtonsoft.Json;

    public class FileService
    {
        /// <summary>
        /// Deserialize the Config into the default config object.
        /// </summary>
        /// <param name="_path">Path of the config</param>
        /// <returns>Config - Default Style</returns>
        protected internal Config GetConfig(string _path)
        {
            string data = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<Config>(data);
        }

        /// <summary>
        /// Deserialize the Config into a custom config object.
        /// </summary>
        /// <param name="_path">Path of the config</param>
        /// <param name="_">Type </param>
        /// <returns></returns>
        protected internal dynamic GetConfig(string _path, Type _)
        {
            string data = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject(data, _);
        }
    }
}
