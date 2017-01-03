namespace Lps.Contracts.ViewModel.Rooms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Booking;
    using Lps.Contracts.ViewModel.Business;
    using Lps.Contracts.ViewModel.Files;
    using Lps.Contracts.ViewModel.Kitchen;
    using Lps.Contracts.ViewModel.Rating;

    public class RoomData
    {                        
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Image.
        /// </summary>
        public string Image { get; set; }

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
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "StreetNumber")]
        public string StreetNumber { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "Plz")]
        public string Plz { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "Phone")]
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
        ///     Gets or sets the role.
        /// </summary>
        public List<PhotoData> Photos { get; set; }

        /// <summary>
        ///  Gets or sets the Main Photo.
        /// </summary>
        public string MainPhoto { get; set; }

        /// <summary>
        ///  Gets or sets the Canvas Image
        /// </summary>
        public string CanvasImage { get; set; }

        /// <summary>
        ///  Gets or sets the Canvas Image
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets the RatingData.
        /// </summary>
        public IList<RatingData> Ratings { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  Gets or sets the IsVisibleBusinessHours
        /// </summary>
        public bool IsVisibleBusinessHours { get; set; }        

        /// <summary>
        /// Gets or sets the KitchenTypes.
        /// </summary>
        public IList<Guid> KitchenTypes { get; set; }

        /// <summary>
        /// Gets or sets the KitchenInternationalTypes.
        /// </summary>
        public IList<Guid> KitchenInternationalTypes { get; set; }

        /// <summary>
        /// Gets or sets the KitchenInternationalTypes.
        /// </summary>
        public IList<SpecializationTypeData> SpecializationTypes { get; set; }

        /// <summary>
        /// Gets or sets the KitchenTypes.
        /// </summary>
        public IList<BusinessHoursData> BusinessHours { get; set; }

        /// <summary>
        /// Gets or sets the KitchenTypes.
        /// </summary>
        public IList<KitchenMenuData> KitchenMenus { get; set; }

        /// <summary>
        /// Gets or sets the RoomTableDataList.
        /// </summary>
        public IList<RoomTableData> RoomTableDataList { get; set; }

        /// <summary>
        ///  Gets or sets the IsChatExist
        /// </summary>
        public bool IsChatExist { get; set; }

        /// <summary>
        ///  Gets or sets the IsFavorite
        /// </summary>
        public bool IsFavorite { get; set; }

        public string SvgLayout { get; set; }

        public string JsonModel { get; set; }

        public string TableLayout { get; set; }

        public float LayoutZoom { get; set; }

        public float RealScaleFactor { get; set; }        


        /// <summary>
        /// Initializes a new instance of the <see cref="RoomData"/> class.
        /// </summary>
        public RoomData()
        {
            this.Photos = new List<PhotoData>();
            this.KitchenTypes = new List<Guid>();
            this.BusinessHours = new List<BusinessHoursData>();
            this.KitchenMenus = new List<KitchenMenuData>();
            this.Ratings = new List<RatingData>();
            this.KitchenInternationalTypes = new List<Guid>();
            this.SpecializationTypes = new List<SpecializationTypeData>();
            this.RoomTableDataList = new List<RoomTableData>();
        }
    }
}
