using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.WebAPI.Models;
using System.Net.Http;
using System.Net;

namespace Core.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NgUserController : ControllerBase
    {
        private readonly IConfiguration config;

        public NgUserController(IConfiguration Config)
        {
            config = Config;
        }

        // GET: api/NgUser
        [HttpGet]
        public ApiResponse Get()
        {
            var Resp = new ApiResponse
            {
                message = "",
                status = 0,
                result = null
            };

            try
            {
                var Mdl = NgUser.Fetch(config.GetConnectionString("db_ABC"));

                Resp.message = "Successful";
                Resp.status = 200;
                Resp.result = Mdl;

                return Resp;
            }
            catch (Exception ex)
            {
                Resp.message = ex.Message;
                Resp.status = 400;
            }

            return Resp;
        }

        // GET: api/NgUser/5
        [HttpGet("{id}", Name = "GetNgUser")]
        public ApiResponse Get(int id)
        {
            var Resp = new ApiResponse
            {
                message = "",
                status = 0,
                result = null
            };

            try
            {
                var Mdl = NgUser.Fetch(config.GetConnectionString("db_ABC"));

                Resp.message = "Successful";
                Resp.status = 200;
                Resp.result = Mdl.FirstOrDefault(t => t.id == id);

                return Resp;
            }
            catch (Exception ex)
            {
                Resp.message = ex.Message;
                Resp.status = 400;
            }

            return Resp;
        }

        // POST: api/NgUser
        [HttpPost]
        public ApiResponse Post([FromBody] NgUser UserModel)
        {
            var Resp = new ApiResponse
            {
                message = "",
                status = 0,
                result = null
            };

            try
            {
                NgUser.Add(config.GetConnectionString("db_ABC"), UserModel);

                Resp.message = "Successful";
                Resp.status = 200;
                Resp.result = UserModel;

                return Resp;
            }
            catch (Exception ex)
            {

                Resp.message = ex.Message;
                Resp.status = 400;
            }

            return Resp;
        }

        // PUT: api/NgUser/5
        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] NgUser UserModel)
        {
            var Resp = new ApiResponse
            {
                message = "",
                status = 0,
                result = null
            };

            try
            {
                UserModel.id = id;
                NgUser.Update(config.GetConnectionString("db_ABC"), UserModel);

                Resp.message = "Successful";
                Resp.status = 200;
                Resp.result = UserModel;

                return Resp;
            }
            catch (Exception ex)
            {
                Resp.message = ex.Message;
                Resp.status = 400;
            }

            return Resp;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("AuthenticateUser")]
        public IActionResult AuthenticateUser([FromBody] LoginRequest login)
        {
            try
            {
                LoginRequest loginrequest = new LoginRequest { };

                loginrequest.Username = login.Username.ToLower();
                loginrequest.Password = login.Password;

                if (loginrequest.Username == "admin" && loginrequest.Password == "admin")
                {
                    string token = NgUser.CreateToken(loginrequest.Username);

                    var Resp = new ApiResponse
                    {
                        message = "Token created successfully",
                        status = 200,
                        result = new
                        {
                            token
                        }
                    };

                    return Ok(Resp);
                }
                else
                {
                    var Resp = new ApiResponse
                    {
                        message = "Token creation failed",
                        status = 200,
                        result = new
                        {
                            token = ""
                        }
                    };

                    return Ok(Resp);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            
        }
    }
}
