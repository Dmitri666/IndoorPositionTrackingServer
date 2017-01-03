using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class Logging
    {
         // Properties
        // Primary key
        public Guid Id { get; set; }

        public string Message { get; set; }
        public string Data { get; set; }
        public string InnerException { get; set; }
        public DateTime Time { get; set; }
        public string InputData { get; set; }
        

        public Logging()
        {            
        }
    }
}
