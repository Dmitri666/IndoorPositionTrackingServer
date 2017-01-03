namespace Lps.Contracts.ViewModel.Rooms
{
    using System;
    using System.Collections.Generic;

    public class RequestLocationData
    {              
        //[JsonIgnore]
        //[ScriptIgnore]
        //public DbGeography Location
        //{
        //    get
        //    {
        //        return default(DbGeography); 
        //        //return Help.PointFromText(this.Latitude, this.Longitude);                
        //    }
        //    set; 
        //}

        /// <summary>
        /// Gets or sets the Radius.
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// Gets or sets the Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the Longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Location Name.
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the is Chat Exist.
        /// </summary>
        public bool IsChatExist { get; set; }

        /// <summary>
        /// Gets or sets the KitchenTypes.
        /// </summary>
        public IList<Guid> KitchenTypes { get; set; }

        /// <summary>
        /// Gets or sets the KitchenInternationalTypes.
        /// </summary>
        public IList<Guid> KitchenInternationalTypes { get; set; }      
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestLocationData"/> class.
        /// </summary>
        public RequestLocationData()
        {
            this.KitchenTypes = new List<Guid>();
            this.KitchenInternationalTypes = new List<Guid>();
        }
    }
}
