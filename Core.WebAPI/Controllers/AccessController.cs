using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.WebAPI.Models;

namespace Core.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private IConfiguration config;

        public AccessController(IConfiguration Config)
        {
            config = Config;
        }


        [HttpGet("GetUsers")]
        public IEnumerable<UserRole> GetUserRoles()
        {
            return UserRole.Fetch(config.GetConnectionString("db_ABC"));
        }
    }
}