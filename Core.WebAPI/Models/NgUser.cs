using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.WebAPI.Models
{
    public class NgUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public double salary { get; set; }

        public static List<NgUser> Fetch(string dbConnection)
        {
            var Model = new List<NgUser>();

            string sql = "select * from [ng_user]";

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
                                var record = new NgUser();

                                record.id = h.GetInt32(0);
                                record.username = h.GetString(1);
                                record.password = h.GetString(2);
                                record.firstName = h.GetString(3);
                                record.lastName = h.GetString(4);
                                record.age = h.GetInt32(5);
                                record.salary = h.GetDouble(6);

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

        public static string CreateToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(7);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token

            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token = (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(subject: claimsIdentity, 
                                                        notBefore: issuedAt, 
                                                        expires: expires, 
                                                        signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public static void Add(string dbConnection, NgUser Model)
        {
            string sql = "insert into [ng_user] values (@username, @pass, @fname, @lname, @age, @salary); select SCOPE_IDENTITY()";

            try
            {
                using (SqlConnection dbCon = new SqlConnection(dbConnection))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sql, dbCon))
                    {
                        dbCom.CommandType = System.Data.CommandType.Text;

                        dbCom.Parameters.AddWithValue("@username", Model.username);
                        dbCom.Parameters.AddWithValue("@pass", Model.password);
                        dbCom.Parameters.AddWithValue("@fname", Model.firstName);
                        dbCom.Parameters.AddWithValue("@lname", Model.lastName);
                        dbCom.Parameters.AddWithValue("@age", Model.age);
                        dbCom.Parameters.AddWithValue("@salary", Model.salary);

                        Model.id = Convert.ToInt32(dbCom.ExecuteScalar());

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void Update(string dbConnection, NgUser Model)
        {
            string sql = "update [ng_user] set firstname=@fname, lastname=@lname, age=@age, salary=@salary where id=@id";

            try
            {
                using (SqlConnection dbCon = new SqlConnection(dbConnection))
                {
                    dbCon.Open();

                    using (SqlCommand dbCom = new SqlCommand(sql, dbCon))
                    {
                        dbCom.CommandType = System.Data.CommandType.Text;

                        dbCom.Parameters.AddWithValue("@fname", Model.firstName);
                        dbCom.Parameters.AddWithValue("@lname", Model.lastName);
                        dbCom.Parameters.AddWithValue("@age", Model.age);
                        dbCom.Parameters.AddWithValue("@salary", Model.salary);
                        dbCom.Parameters.AddWithValue("@id", Model.id);

                        dbCom.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public LoginResponse()
        {

            this.Token = "";
            this.responseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }

        public string Token { get; set; }
        public HttpResponseMessage responseMsg { get; set; }

    }

    public class ApiResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}
