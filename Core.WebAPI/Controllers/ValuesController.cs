using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Core.WebAPI.Models;

namespace Core.WebAPI.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };      
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value: " + id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// call:https://localhost:44363/api/values/Fetch?a=7&b=0
        /// or call: [Host]/api/values/Fetch?a=7&b=0
        [HttpGet("Fetch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> FetchById(string a, string b)
        {

            if (a == "7")
            {
                return NotFound();
            }

            return Ok(new Product());
        }

        //call: https://localhost:44363/api/values/FetchVal?m=1
        //call: [host]/api/values/FetchVal?m=1 if httpget verb has template = "FetchVal"
        //call: [host]/api/values/FetchVal/5 (where h matches with param h) if httpget verb has template = "FetchVal/{h}"
        //
        [HttpGet("FetchVal/{h}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> FetchByValue(int h)
        {

            if (h == 8)
            {
                return NotFound();
            }

            return Ok(new Product());
        }
    }
}
