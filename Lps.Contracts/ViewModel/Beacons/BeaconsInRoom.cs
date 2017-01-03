using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lps.Contracts.ViewModel.Beacons
{
    public class BeaconsInRoom
    {
        public Guid RoomId { get; set; }

        public List<BeaconInRoom> Beacons { get; set; }

    }
}
