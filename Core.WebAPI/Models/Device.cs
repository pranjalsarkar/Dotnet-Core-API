using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebAPI.Models
{
    public class Device
    {
        public string serialNo { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public static IEnumerable<Device> SearchDevice(string searchText)
        {
            var src = new List<Device> { new Device {
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

            return src.Where(t => t.name.ToLower().Contains(searchText) || t.type.ToLower().Contains(searchText));
        }
    }

    public class Employee
    {
        public string empNo { get; set; }
        public string empName { get; set; }
        public string jobTitle { get; set; }
    }

    public class SearchDevice
    {
        public string searchText { get; set; }
        public DateTime searchDate { get; set; }
    }


}
