namespace Lps.Services
{
    using LpsServer.Data.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Files;
    using System.Data.Entity.Spatial;
    using System.Globalization;
    using Lps.Contracts.ViewModel.Booking;
    using Lps.Contracts.ViewModel.Business;
    using Lps.Contracts.ViewModel.Kitchen;
    using Lps.Contracts.ViewModel.Rating;
    using Lps.Contracts.ViewModel.Rooms;    

    public static class Help
    {
        /// <summary>
        /// Convert Miles To Meters
        /// </summary>
        /// <param name="miles"></param>
        /// <returns></returns>
        public static double ConvertMilesToMeters(int? miles)
        {
            if (miles.HasValue)
            {
                //return miles.Value * 1609.344;
                return miles.Value * 1000;
            }

            return 0;
        }

        /// <summary>
        /// Create a GeoLocation point based on latitude and longitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat, "POINT({0} {1})", longitude, latitude);

            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(text, 4326);
        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

        public static TypeaheadLocationData CopyProperties(this Room existingRoom)
        {
            TypeaheadLocationData typeaheadLocationData = new TypeaheadLocationData();
            typeaheadLocationData.Id = existingRoom.Id;
            typeaheadLocationData.Description = existingRoom.Name;
            
            return typeaheadLocationData;
        }

        public static TypeaheadCityData CopyProperties(this Room existingRoom, bool isCity = true)
        {
            TypeaheadCityData typeaheadLocationData = new TypeaheadCityData();
            typeaheadLocationData.Id = existingRoom.Id;
            typeaheadLocationData.Description = existingRoom.City;

            return typeaheadLocationData;
        }              

        public static RoomTableData CopyProperties(this RoomTable existingRoomTable)
        {
            RoomTableData roomTableData = new RoomTableData();
                        
            roomTableData.Id = existingRoomTable.Id;
            roomTableData.Description = existingRoomTable.Description;
            roomTableData.Type = existingRoomTable.Type;

            return roomTableData;
        }       

        public static Room CopyProperties(this Room existingRoom, RoomData table, User CurrentUser)
        {
            existingRoom.Time = DateTime.Now;
            existingRoom.User = CurrentUser;
            existingRoom.Latitude = table.Latitude;
            existingRoom.Longitude = table.Longitude;
            existingRoom.Location = Help.CreatePoint(table.Latitude, table.Longitude);
            existingRoom.Name = table.Name;            
            existingRoom.Street = table.Street;            
            existingRoom.StreetNumber = table.StreetNumber;
            existingRoom.City = table.City;
            existingRoom.Plz = table.Plz;
            existingRoom.Phone = table.Phone;
            existingRoom.Email = table.Email;
            existingRoom.Homepage = table.Homepage;
            existingRoom.IsVisibleBusinessHours = table.IsVisibleBusinessHours;
            existingRoom.Description = table.Description;

            return existingRoom;
        }

        public static Rating CopyProperties(this Rating existingRating, RatingData table, User CurrentUser)
        {
            existingRating.Time = DateTime.Now;
            existingRating.User = CurrentUser;
            existingRating.Description = table.Description;
            existingRating.State = table.State;

            return existingRating;
        }
        
        public static UserData CopyProperties(this User user)
        {
            UserData userData = new UserData();

            userData.Id = user.Id;
            userData.Password = user.Password;
            userData.UserName = user.Name;
            userData.Roles = user.UserRoleList.Select(m => m.RoleType.Name).ToList();
            userData.MainUserRole = user.UserRoleList.OrderBy(m => m.RoleType.Order).Select(m => m.RoleType.Name).FirstOrDefault() ?? string.Empty;
            userData.Company = user.Company;
            userData.Email = user.Email;
            userData.Photo = user.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault() ?? string.Empty;
            userData.Photos = user.PhotoList.Select(m => m.CopyProperties()).ToList();

            return userData;
        }

        public static User CopyProperties(this User user, UserData userData)
        {            
            user.Password = userData.Password;
            user.Name = userData.UserName;            
            user.Email = userData.Email;
            user.Company = userData is UserBarOwnerData ? (userData as UserBarOwnerData).Company : "";
            user.ProviderKey = userData.ProviderKey;
            user.LoginProvider = userData.LoginProvider;

            return user;
        }

        public static KitchenTypeData CopyProperties(this KitchenType kitchenType)
        {
            KitchenTypeData kitchenTypeData = new KitchenTypeData();

            kitchenTypeData.Id = kitchenType.Id;
            kitchenTypeData.Name = kitchenType.Name;
            kitchenTypeData.Description = kitchenType.Description;
            kitchenTypeData.Order = kitchenType.Order;

            return kitchenTypeData;
        }

        public static KitchenMenuTypeData CopyProperties(this KitchenMenuType kitchenMenuType)
        {
            KitchenMenuTypeData kitchenMenuTypeData = new KitchenMenuTypeData();

            kitchenMenuTypeData.Id = kitchenMenuType.Id;
            kitchenMenuTypeData.Name = kitchenMenuType.Name;
            kitchenMenuTypeData.Description = kitchenMenuType.Description;
            kitchenMenuTypeData.Order = kitchenMenuType.Order;

            return kitchenMenuTypeData;
        }

        public static KitchenInternationalTypeData CopyProperties(this KitchenInternationalType kitchenInternationalType)
        {
            KitchenInternationalTypeData kitchenInternationalTypeData = new KitchenInternationalTypeData();

            kitchenInternationalTypeData.Id = kitchenInternationalType.Id;
            kitchenInternationalTypeData.Name = kitchenInternationalType.Name;
            kitchenInternationalTypeData.Description = kitchenInternationalType.Description;
            kitchenInternationalTypeData.Order = kitchenInternationalType.Order;
            kitchenInternationalTypeData.ParentId = kitchenInternationalType.ParentId;

            return kitchenInternationalTypeData;
        }

        public static SpecializationTypeData CopyProperties(this SpecializationType specializationType)
        {
            SpecializationTypeData specializationTypeData = new SpecializationTypeData();

            specializationTypeData.Id = specializationType.Id;
            specializationTypeData.ParentId = specializationType.ParentId;
            specializationTypeData.Hierarchie = specializationType.Hierarchie;
            specializationTypeData.Name = specializationType.Name;
            specializationTypeData.Description = specializationType.Description;
            specializationTypeData.Order = specializationType.Order;            

            return specializationTypeData;
        }

        public static KitchenMenuData CopyProperties(this KitchenMenu kitchenMenu)
        {
            KitchenMenuData kitchenMenuData = new KitchenMenuData();

            kitchenMenuData.Id = kitchenMenu.Id;
            kitchenMenuData.Name = kitchenMenu.Name;
            kitchenMenuData.Description = kitchenMenu.Description;
            kitchenMenuData.Order = kitchenMenu.Order;
            kitchenMenuData.Price = kitchenMenu.Price;
            kitchenMenuData.KitchenMenuTypeId = kitchenMenu.KitchenMenuType.Id;
            kitchenMenuData.KitchenMenuTypeName = kitchenMenu.KitchenMenuType.Name;

            return kitchenMenuData;
        }

        public static BookingJoinRoomData CopyProperties(this LpsServer.Data.Entities.Booking booking)
        {
            BookingJoinRoomData bookingJoinRoomData = new BookingJoinRoomData();

            //Booking
            bookingJoinRoomData.BookingId = booking.Id;
            bookingJoinRoomData.Time = booking.Time;
            bookingJoinRoomData.PeopleCount = booking.PeopleCount;
            bookingJoinRoomData.State = booking.State;
            bookingJoinRoomData.UserName = booking.User.Name;
            bookingJoinRoomData.RoomTableDataList = booking.Tables.Select(m => m.RoomTable.CopyProperties()).ToList();
            bookingJoinRoomData.CreateTime = booking.CreateTime;

            //Room
            bookingJoinRoomData.RoomId = booking.Tables.FirstOrDefault().RoomTable.Room.Id;
            bookingJoinRoomData.Name = booking.Tables.FirstOrDefault().RoomTable.Room.Name;            
            bookingJoinRoomData.MainPhoto = booking.Tables.FirstOrDefault().RoomTable.Room.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault() ?? string.Empty;
            
            return bookingJoinRoomData;
        }

        public static RoomData CopyProperties(this Room room, User user)
        {
            RoomData roomData = new RoomData();

            roomData.Id = room.Id;
            roomData.Latitude = room.Latitude;
            roomData.Longitude = room.Longitude;
            roomData.Name = room.Name;
            roomData.Street = room.Street;
            roomData.StreetNumber = room.StreetNumber;
            roomData.City = room.City;
            roomData.Plz = room.Plz;
            roomData.Phone = room.Phone;
            roomData.Email = room.Email;
            roomData.Homepage = room.Homepage;
            roomData.Photos = room.PhotoList.Select(m => m.CopyProperties()).ToList();
            roomData.MainPhoto = room.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault() ?? string.Empty;
            roomData.CanvasImage = room.CanvasImage;            
            roomData.BusinessHours = room.BusinessHoursList.Select(m => m.CopyProperties()).ToList();
            roomData.IsVisibleBusinessHours = room.IsVisibleBusinessHours;
            roomData.KitchenMenus = room.KitchenMenuList.Select(m => m.CopyProperties()).ToList();
            roomData.Rating = room.RatingList.Select(m => m.State).DefaultIfEmpty().Average();
            roomData.Ratings = room.RatingList.Select(m => m.CopyProperties()).ToList();
            roomData.Description = room.Description;
            roomData.IsChatExist = room.IsChatExist;
            roomData.KitchenTypes = room.RoomKitchenList.Select(m => m.KitchenType.Id).ToList();            
            roomData.SpecializationTypes = room.SpecializationList.Select(m => m.SpecializationType.CopyProperties()).ToList();
            roomData.KitchenInternationalTypes = room.RoomKitchenInternationalList.Select(m => m.KitchenInternationalType.Id).ToList();
            roomData.IsFavorite = user != null && room.FavoritsList.Any(x => x.User.Id == user.Id);
            roomData.SvgLayout = room.SvgLayout;
            roomData.RoomTableDataList = room.Tables.Select(m => m. CopyProperties()).ToList();


            return roomData;
        }       

        public static RatingData CopyProperties(this Rating rating)
        {
            RatingData ratingData = new RatingData();

            ratingData.Id = rating.Id;
            ratingData.Description = rating.Description;
            ratingData.State = rating.State;
            ratingData.Time = rating.Time;
            ratingData.UserName = rating.User.Name;

            return ratingData;
        }

        public static BusinessHoursData CopyProperties(this BusinessHours existing)
        {
            BusinessHoursData businessHoursData = new BusinessHoursData();

            businessHoursData.Close = existing.Close;
            businessHoursData.Day = existing.Day;                        
            businessHoursData.PauseEnd = existing.PauseEnd.HasValue ? existing.PauseEnd.Value.ToString("HH:mm", CultureInfo.InvariantCulture).ToLower() : "";
            businessHoursData.PauseStart = existing.PauseStart.HasValue ? existing.PauseStart.Value.ToString("HH:mm", CultureInfo.InvariantCulture).ToLower() : "";
            businessHoursData.OpenTime = existing.OpenTime.ToString("HH:mm", CultureInfo.InvariantCulture).ToLower();
            businessHoursData.CloseTime = existing.CloseTime.ToString("HH:mm", CultureInfo.InvariantCulture).ToLower();
                        
            return businessHoursData;
        }

        public static KitchenMenu CopyProperties(this KitchenMenuData existing, KitchenMenu kitchenMenu, KitchenMenuType kitchenMenuType)
        {
            kitchenMenu.Name = existing.Name;
            kitchenMenu.Description = existing.Description;
            kitchenMenu.Order = existing.Order;
            kitchenMenu.Price = existing.Price;
            kitchenMenu.KitchenMenuType = kitchenMenuType;

            return kitchenMenu;
        }

        public static BusinessHours CopyProperties(this BusinessHoursData existing, BusinessHours businessHours)
        {                 
            businessHours.Day = existing.Day;            
            businessHours.OpenTime = DateTime.Parse(existing.OpenTime);
            businessHours.CloseTime = DateTime.Parse(existing.CloseTime);
            businessHours.PauseEnd = string.IsNullOrEmpty(existing.PauseEnd) ? (DateTime?)null : DateTime.Parse(existing.PauseEnd);
            businessHours.PauseStart = string.IsNullOrEmpty(existing.PauseStart) ? (DateTime?)null : DateTime.Parse(existing.PauseStart);            
            businessHours.Close = existing.Close;                   

            return businessHours;
        }

        public static PhotoData CopyProperties(this Photo existing)
        {
            PhotoData photoData = new PhotoData();
            photoData.Id = existing.Id;
            photoData.Image = existing.Image;
            photoData.IsMain = existing.IsMain;
            photoData.Time = existing.Time;

            return photoData;
        }

        public static PhotoData CopyProperties(this UserPhoto existing)
        {
            PhotoData photoData = new PhotoData();
            photoData.Id = existing.Id;
            photoData.Image = existing.Image;
            photoData.IsMain = existing.IsMain;
            photoData.Time = existing.Time;

            return photoData;
        }

        public static RoomTable CopyProperties(this RoomTable existing, RoomTable table) 
        {
            existing.X = table.X;
            existing.Y = table.Y;
            existing.Description = table.Description;
            existing.Angle = table.Angle;

            return existing;
        }                 
    }    
}
