// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Room.cs" company="">
//   
// </copyright>
// <summary>
//   The room.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Spatial;

    /// <summary>
    ///     The room.
    /// </summary>
    public class Room
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Room" /> class.
        /// </summary>
        public Room()
        {
            this.BeaconList = new HashSet<Beacon>();
            this.PositionList = new HashSet<Position>();
            this.PhotoList = new HashSet<Photo>();
            this.Tables = new HashSet<RoomTable>();
            this.RoomKitchenList = new HashSet<RoomKitchen>();
            this.BusinessHoursList = new HashSet<BusinessHours>();
            this.KitchenMenuList = new HashSet<KitchenMenu>();
            this.RatingList = new HashSet<Rating>();
            this.RoomKitchenInternationalList = new HashSet<RoomKitchenInternational>();  
            this.FavoritsList = new HashSet<Favorits>();
            this.SpecializationList = new HashSet<Specialization>();            
        }

        #endregion

        // Properties
        #region Public Properties

        /// <summary>
        /// Gets or sets the user device.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Tables
        /// </summary>
        public virtual ICollection<RoomTable> Tables { get; set; }

        /// <summary>
        ///     Gets or sets the beacon list.
        /// </summary>
        public virtual ICollection<Beacon> BeaconList { get; set; }

        public float RoomHeight { get; set; }

        public float RoomWidth { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the lng.
        /// </summary>
        public double Longitude { get; set; }
        
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Location.
        /// </summary>
        public DbGeography Location { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string StreetNumber { get; set; }
        
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Plz { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Email { get; set; }     

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Homepage { get; set; }     

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Gets or sets the Canvas Image.
        /// </summary>
        public string CanvasImage { get; set; }

        /// <summary>
        ///     Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets the IsVisibleBusinessHours
        /// </summary>
        public bool IsVisibleBusinessHours { get; set; }

        /// <summary>
        ///     Gets or sets the Is Chat Exist.
        /// </summary>
        public bool IsChatExist { get; set; }

        /// <summary>
        /// Gets or sets the BusinessHours list.
        /// </summary>
        public virtual ICollection<BusinessHours> BusinessHoursList { get; set; }

        /// <summary>
        /// Gets or sets the position list.
        /// </summary>
        public virtual ICollection<Position> PositionList { get; set; }

        /// <summary>
        /// Rooms
        /// </summary>
        public virtual ICollection<Photo> PhotoList { get; set; }

        /// <summary>
        ///     Gets or sets the RoomK itchen
        /// </summary>
        public virtual ICollection<RoomKitchen> RoomKitchenList { get; set; }

        /// <summary>
        ///     Gets or sets the Specialization
        /// </summary>
        public virtual ICollection<Specialization> SpecializationList { get; set; }

        /// <summary>
        ///     Gets or sets the RoomK itchen
        /// </summary>
        public virtual ICollection<RoomKitchenInternational> RoomKitchenInternationalList { get; set; }

        /// <summary>
        ///     Gets or sets the KitchenMenu
        /// </summary>
        public virtual ICollection<KitchenMenu> KitchenMenuList { get; set; }

        /// <summary>
        ///     Gets or sets the RatingList
        /// </summary>
        public virtual ICollection<Rating> RatingList { get; set; }

        /// <summary>
        ///     Gets or sets the FavoritsList
        /// </summary>
        public virtual ICollection<Favorits> FavoritsList { get; set; }

        public string SvgLayout { get; set; }

        public string JsonModel { get; set; }

        public string TableLayout { get; set; }

        public float LayoutZoom { get; set; }

        public float RealScaleFactor { get; set; }
        
        public string BackgroungImage { get; set; }

        #endregion
    }
}