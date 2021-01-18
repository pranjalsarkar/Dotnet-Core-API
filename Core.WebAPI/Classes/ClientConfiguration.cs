using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebAPI.Classes
{
    public interface IClientConfiguration
    {
        string ClientName { get; set; }
        DateTime InvokedDateTime { get; set; }
    }

    public class ClientConfiguration : IClientConfiguration
    {
        public string ClientName { get; set; }
        public DateTime InvokedDateTime { get; set; }
    }
}
