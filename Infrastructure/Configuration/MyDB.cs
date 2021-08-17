using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configuration
{
    public class infConnection
    {
        public string conexao { get; set; }
        public string db { get; set; }
    }
    public class MyDB
    {
        public static MyDB instance;
        private static IConfiguration _config;
        public MyDB(IConfiguration config)
        {
            _config = config;
        }

        private infConnection _infConnection { get; set; }

        public infConnection getStringConn()
        {
            if (_infConnection == null)
            {
                _infConnection = new infConnection();
                _infConnection.conexao = _config["conexao:connectionString"];
                _infConnection.db = _config["conexao:db"];
            }

            return _infConnection;
        }
    }
}
