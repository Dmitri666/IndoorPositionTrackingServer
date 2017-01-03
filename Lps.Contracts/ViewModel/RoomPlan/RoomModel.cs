namespace Lps.Contracts.ViewModel.RoomPlan
{
    using System;
    using System.Collections.Generic;

    public class RoomModel
    {
        public Guid Id { get; set; }

        public string Json { get; set; }

        public float Zoom { get; set; }

        public float RealScaleFactor { get; set; }

        public string SvgLayout { get; set; }

        public string TablesLayout { get; set; }

        public List<Beacon> Beacons { get; set; }

        public List<Table> Tables { get; set; }

        public string BackgroundImage { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }
    }
}
