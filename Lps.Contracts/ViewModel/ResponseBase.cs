using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel
{
    public class ResponseBase
    {
        public bool Success { get; set; }

        public Guid RoomId { get; set; }
    }
}
