﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace EMS.Models
{
    /// <summary>
    /// Opens a databsse connection
    /// </summary>
    internal class DBConnection
    {
        private string? _server;
        private string? _database;
        private string? _userName;
        private string? _password;
        private string? _conn;

        public string Server
        {
            set { _server = value; }
        }

        public string Database
        {
            set { _database = value; }
        }

        public string UserName
        {
            set { _userName = value; }
        }

        public string Password
        {
            set { _password = value; }
        }

        public string Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        private void BindConnectionVariables()
        {
            Server = "107.180.27.178";
            Database = "EMS";
            UserName = "oopadmin";
            Password = "taQoCt]ApQZ5";
        }


        public void Connect()
        {
            this.BindConnectionVariables();
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = _server,
                UserID = _userName,
                Password = _password,
                Database = _database
            };

            // Connecting to database
            using (MySqlConnection conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                Console.WriteLine($"Connected to {_database} database!");

                string sql = "select * from category_list";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);

                }

                conn.Close();
            }
        }
    }
}
