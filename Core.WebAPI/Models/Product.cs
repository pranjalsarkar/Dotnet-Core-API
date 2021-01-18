using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Core.WebAPI;
using Microsoft.AspNetCore.Http;

namespace Core.WebAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Stock
    {
        public int Id { get; set; }
        //[Required]
        //[MaxLength(10, ErrorMessage ="Name can only be 10 letters")]
        public string Name { get; set; }
       // [Required]
        public double Quantity { get; set; }
       // [Required]
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        //const string DbConnectionString = Confi ["ConnectionStrings:db_ABC"];

        public static List<Stock> Fetch(string dbConnection)
        {
            var Model = new List<Stock>();

            string sql = "select * from [stock]";

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
                                var record = new Stock();

                                record.Id = h.GetInt32(0);
                                record.Name = h.GetString(1);
                                record.Quantity = h.GetDouble(2);
                                record.UnitPrice = h.GetDouble(3);
                                record.TotalPrice = h.GetDouble(4);

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

        public static Stock Add(string dbConnection, Stock Model)
        {
            string sql = "insert into [stock] values (@name, @quantity, @unitprice, @totalprice); select SCOPE_IDENTITY()";

            try
            {
                using (SqlConnection dbCon = new SqlConnection(dbConnection))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sql, dbCon))
                    {
                        dbCom.CommandType = System.Data.CommandType.Text;

                        dbCom.Parameters.AddWithValue("@name", Model.Name ?? string.Empty);
                        dbCom.Parameters.AddWithValue("@quantity", Model.Quantity);
                        dbCom.Parameters.AddWithValue("@unitprice", Model.UnitPrice);
                        dbCom.Parameters.AddWithValue("@totalprice", Model.TotalPrice);

                        Model.Id = Convert.ToInt32(dbCom.ExecuteScalar());

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Model;

        }

        public static void Update(string dbConnection, Stock Model)
        {
            string sql = "update [stock] set name=@name, quantity=@quantity, unitprice=@unitprice, totalprice=@totalprice where itemid=@id";

            try
            {
                using (SqlConnection dbCon = new SqlConnection(dbConnection))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sql, dbCon))
                    {
                        dbCom.CommandType = System.Data.CommandType.Text;

                        dbCom.Parameters.AddWithValue("@name", Model.Name ?? string.Empty);
                        dbCom.Parameters.AddWithValue("@quantity", Model.Quantity);
                        dbCom.Parameters.AddWithValue("@unitprice", Model.UnitPrice);
                        dbCom.Parameters.AddWithValue("@totalprice", Model.TotalPrice);
                        dbCom.Parameters.AddWithValue("@id", Model.Id);

                        dbCom.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public int Size { get; set; }
        public string UserId { get; set; }
        public string DocumentType { get; set; }


    }
    public class FileInputModel
    {
        public IFormFile File { get; set; }
        public string UserName { get; set; }
        public string DocType { get; set; }
    }

    //DI test

    public interface ISave
    {
        void Save();
    }
    public interface IUpdate
    {
        void Update();
    }
    public class Main : ISave, IUpdate
    {
        public void Save()
        {
            //throw new NotImplementedException();
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
