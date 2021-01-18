using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.WebAPI.Models;
using Microsoft.Extensions.Logging;
using Core.WebAPI.Classes;

namespace Core.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        //PS: inject configuration 
        //asp.net core discourages using statics for configuration data (leads to all the bad practices in asp.net classic). 
        //it should be passed as construction parameter, either directly or via DI. 

        private readonly IConfiguration _config;
        private readonly ISave _save;
        private readonly IUpdate _update;
        private readonly ILogger _logger;
        private readonly IClientConfiguration _clientConfig;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public StockController(IConfiguration config, ISave save, IUpdate update, ILogger<StockController> logger, IClientConfiguration clientConfig)
        {
            _save = save;
            _update = update;
            _config = config;
            _logger = logger;
            _clientConfig = clientConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Stock
        [HttpGet]
        public IEnumerable<Stock> Get()
        {
            _save.Save();
            _update.Update();

            _logger.LogInformation("GET method called");

            return Stock.Fetch(_config.GetConnectionString("db_ABC"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Stock/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        // POST: api/Stock
        [HttpPost]
        public Stock Post([FromBody] Stock Model)
        {
            return Stock.Add(_config.GetConnectionString("db_ABC"), Model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Model"></param>
        // PUT: api/Stock/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stock Model)
        {
            Model.Id = id;  //we can also pass id in the model from client 'PUT' call
            Stock.Update(_config.GetConnectionString("db_ABC"), Model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getClientConfig")]
        public ActionResult<ClientConfiguration> GetClientConfiguration()
        {
            return new ClientConfiguration
            {
                ClientName = _clientConfig.ClientName,
                InvokedDateTime = _clientConfig.InvokedDateTime
            };
        }
    }
}
