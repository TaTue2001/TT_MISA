using Dapper;
using MISA.core.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.infrastructure.Repos
{
    public class BaseRepository<MISA_entity>
    {
        protected readonly string ConnectionString = "Host=127.0.0.1;Port=3306;Database=misa_web;User Id=root; Password=tue@2001";
        protected MySqlConnection Connection;


        public IEnumerable<MISA_entity> GetAll()
        {
            var className=typeof(MISA_entity).Name;
            using (Connection = new MySqlConnection(ConnectionString))
            {   
                
                //2 lay du lieu tu database
                //2.cau lenh truy van du lieu
                var query = $"select*from {className}";
                //2.2 thuc hien lay du lieu
                var entities = Connection.Query<MISA_entity>(sql: query);
                return entities;
            }
        }
    }
}
