using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class RoomKitchen
    {
        public Guid Id { get; set; }

        public virtual Room Room { get; set; }

        public virtual KitchenType KitchenType { get; set; }
    }
}
