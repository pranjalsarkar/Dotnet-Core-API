using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        // GET: api/Device
        [HttpGet]
        public IEnumerable<Device> Get()
        {
            return new List<Device> { new Device {
                                        serialNo= "8f67699f-66af-1647-6ded-8528a675008e",
                                        name= "Laptop",
                                        type= "Computer"
                                      },
                                      new Device
                                      {
                                        serialNo= "c765fedb-6730-b53b-d609-a32665e93412",
                                        name= "Desktop",
                                        type= "Computer"
                                      },
                                      new Device
                                      {
                                        serialNo= "96c71fed-08c2-e2cf-7a55-94dcc8615a9c",
                                        name= "Tablet",
                                        type= "Computer"
                                      },
                                      new Device
                                      {
                                        serialNo= "e06275bf-080c-c6ff-c373-4f53807158f3",
                                        name= "iPhone",
                                        type= "Computer"
                                      }};
        }

        // GET: api/Device/5
        [HttpGet("{id}", Name = "GetDeviceById")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Device
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Device/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        //Search
        [HttpPost("SearchDevice")]
        public IEnumerable<Device> SearchDevice([FromBody] SearchDevice searchModel)
        {
            return Device.SearchDevice(searchModel.searchText);
        }
    }
}
