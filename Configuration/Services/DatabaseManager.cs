namespace BloodyBotSystem.Configuration.Services
{
    using System;
    using System.Data.Common;
    using MySql.Data.MySqlClient;

    public class DatabaseManager
    {
        private readonly BloodyBot bot;

        public DatabaseManager(BloodyBot _bot)
        {
            this.bot = _bot;
        }

        private MySqlConnection DbConnect(string _host, int _port, string _database, string _username, string _password)
        {
            // Connection String.
            string connString = "Server=" + _host + ";Database=" + _database + ";port=" + _port + ";User Id=" + _username + ";password=" + _password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

        private MySqlConnection GetDbConnection(string _mySqlHost, string _mySqlDatabase, string _mySqlUsername, string _mySqlPass, int _mySqlPort)
        {
            return DbConnect(_mySqlHost, _mySqlPort, _mySqlDatabase, _mySqlUsername, _mySqlPass);
        }

        private MySqlConnection MysqlConnection(int _dbIndex = 0)
        {

            MySqlConnection conn = GetDbConnection(bot.Config.MySql[_dbIndex].MySqlHost, bot.Config.MySql[_dbIndex].MySqlDb, bot.Config.MySql[_dbIndex].MySqlUser,
                                                   bot.Config.MySql[_dbIndex].MySqlPass, bot.Config.MySql[_dbIndex].MySqlPort);
            conn.Open();
            return conn;
        }

        //Main
        protected internal void VoidQuery(string _queryCommand, int _dbIndex = 0)
        {
            // Create command.
            MySqlCommand cmd = new MySqlCommand { Connection = MysqlConnection(_dbIndex) };
            // Set connection for command.

            try
            {
                cmd.CommandText = _queryCommand;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                // Close connection.
                MysqlConnection(_dbIndex).Close();
                // Dispose object, Freeing Resources.
                MysqlConnection(_dbIndex).Dispose();
            }
        }

        protected internal object ObjectQuery(string _queryCommand, int _dbIndex = 0)
        {
            object sqlResult = null;

            //create command
            MySqlCommand cmd = new MySqlCommand { Connection = MysqlConnection(_dbIndex), CommandText = _queryCommand };

            try
            {
                using DbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sqlResult = reader.GetString(0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                //close connection
                MysqlConnection(_dbIndex).Close();
                //Dispose of object, freeing resources
                MysqlConnection(_dbIndex).Dispose();
            }

            return sqlResult;
        }
    }
}


