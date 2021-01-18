using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebAPI.Models
{
    public class UserRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }

        public static List<UserRole> Fetch(string dbConnection)
        {
            var Model = new List<UserRole>();

            string sql = "select rlskey, role, roledesc, isadm from [rls]";

            try
            {
                using (SqlConnection dbCon = new SqlConnection(dbConnection))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sql, dbCon))
                    {
                        dbCom.CommandType = CommandType.Text;

                        using (SqlDataReader h = dbCom.ExecuteReader())
                        {
                            while (h.Read())
                            {
                                var record = new UserRole();

                                record.Id = h.GetString(0);
                                record.Name = h.GetString(1);
                                record.Description = h.GetString(2);
                                record.IsAdmin = h.GetBoolean(3);

                                Model.Add(record);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Model;
        }
    }
}
