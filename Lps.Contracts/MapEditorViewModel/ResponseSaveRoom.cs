using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.MapEditorViewModel
{
    public class ResponseSaveRoom
    {
        public Guid RoomId { get; set; }

        public bool Success { get; set; }
    }
}
