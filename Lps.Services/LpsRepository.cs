// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LpsRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The room repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using Lps.Contracts.MapEditorViewModel;
    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Booking;
    using Lps.Contracts.ViewModel.Chat;
    using Lps.Contracts.ViewModel.Files;
    using Lps.Contracts.ViewModel.Kitchen;
    using Lps.Contracts.ViewModel.Rating;
    using Lps.Contracts.ViewModel.Rooms;
    using Lps.Contracts.ViewModel.RoomPlan;
    using Lps.Services.Booking;
    using LpsServer.Data;
    using LpsServer.Data.Entities;
    using Contracts.Helper;

    /// <summary>
    ///     The room repository.
    /// </summary>
    public class LpsRepository : IDisposable
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LpsRepository" /> class.
        ///     Prevents a default instance of the <see cref="LpsRepository" /> class from being created.
        ///     Initializes a new instance of the <see cref="LpsRepository" /> class.
        /// </summary>
        /// <param name="ctx">
        ///     The ctx.
        /// </param>
        public LpsRepository()
        {
            this.Context = new LpsContext();
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="LpsRepository" /> class.
        /// </summary>
        ~LpsRepository()
        {
            this.Context.Dispose();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the current user.
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return this.Context.Users.FirstOrDefault(x => x.Name == ClaimsPrincipal.Current.Identity.Name);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the context.
        /// </summary>
        private LpsContext Context { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// AddPhoto
        /// </summary>
        /// <param name="photoDataRequest">
        /// </param>
        /// <returns>
        /// The <see cref="PhotoDataResponse"/>.
        /// </returns>
        public PhotoDataResponse AddPhotoToUser(PhotoDataRequest photoDataRequest)
        {
            if (photoDataRequest == null || string.IsNullOrEmpty(photoDataRequest.Image))
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            Guid imageId = Guid.Parse(Path.GetFileNameWithoutExtension(photoDataRequest.Image));

            // wird es zu dem User hinzugefügt
            var newPhoto = this.Context.UserPhotos.Create();
            newPhoto.Id = imageId;
            newPhoto.IsMain = photoDataRequest.isMain;
            newPhoto.Image = photoDataRequest.Image;
            newPhoto.Time = DateTime.Now;

            this.CurrentUser.PhotoList.Add(newPhoto);

            return new PhotoDataResponse
            {
                Success = this.Context.SaveChanges() > 0,
                Photo = new PhotoData
                {
                    Id = newPhoto.Id,
                    Image = newPhoto.Image
                }
            };
        }

        /// <summary>
        /// DeletePhotoFromUser
        /// </summary>
        /// <param name="deletePhotoBase"></param>
        /// <returns></returns>
        public PhotoDataResponse DeletePhotoFromUser(DeletePhotoBase deletePhotoBase)
        {
            if (deletePhotoBase == null || deletePhotoBase.PhotoId == Guid.Empty)
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            var existPhoto = this.Context.UserPhotos.FirstOrDefault(x => x.Id == deletePhotoBase.PhotoId);
            if (existPhoto != null)
            {
                this.Context.UserPhotos.Remove(existPhoto);

                return new PhotoDataResponse
                {
                    Success = this.Context.SaveChanges() > 0
                };
            }

            return new PhotoDataResponse
            {
                Success = false
            };
        }

        /// <summary>
        /// IsUserExistByProviderKey
        /// </summary>
        /// <param name="loginProvider"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public bool IsUserExistByProviderKey(string loginProvider, string providerKey)
        {
            if (string.IsNullOrEmpty(loginProvider) || string.IsNullOrEmpty(providerKey))
            {
                return false;
            }

            var existUser = this.Context.Users.FirstOrDefault(m => m.ProviderKey == providerKey && m.LoginProvider == loginProvider);

            return existUser != null;
        }

        /// <summary>
        /// GetUserByProviderKey
        /// </summary>
        /// <param name="loginProvider"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public UserData GetUserByProviderKey(string loginProvider, string providerKey)
        {
            if (string.IsNullOrEmpty(loginProvider) || string.IsNullOrEmpty(providerKey))
            {
                return null;
            }

            
            var existUser = this.Context.Users.FirstOrDefault(m => m.ProviderKey == providerKey && m.LoginProvider == loginProvider);
            if (existUser != null)
            {
                var userData = existUser.CopyProperties();

                return userData;
            }

            return null;
        }

        /// <summary>
        /// SetUserMainPhoto
        /// </summary>
        /// <param name="setMainPhotoRequest"></param>
        /// <returns></returns>
        public PhotoDataResponse SetUserMainPhoto(SetMainPhotoBase setMainPhotoBase)
        {
            if (setMainPhotoBase == null || setMainPhotoBase.PhotoId == Guid.Empty)
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            var existPhoto = this.Context.UserPhotos.FirstOrDefault(x => x.Id == setMainPhotoBase.PhotoId);
            if (existPhoto != null)
            {
                existPhoto.IsMain = setMainPhotoBase.IsMain;

                return new PhotoDataResponse
                {
                    Success = this.Context.SaveChanges() > 0
                };
            }

            return new PhotoDataResponse
            {
                Success = false
            };
        }

        /// <summary>
        /// The delete favorit.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        public PhotoDataResponse SetMainPhoto(SetMainPhotoRequest setMainPhotoRequest)
        {
            if (setMainPhotoRequest == null ||
                setMainPhotoRequest.RoomId == Guid.Empty ||
                setMainPhotoRequest.PhotoId == Guid.Empty)
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            var existRoom = this.Context.Rooms.FirstOrDefault(x => x.Id == setMainPhotoRequest.RoomId);
            if (existRoom != null)
            {
                var existPhoto = existRoom.PhotoList.FirstOrDefault(x => x.Id == setMainPhotoRequest.PhotoId);
                if (existPhoto != null)
                {
                    existPhoto.IsMain = setMainPhotoRequest.IsMain;

                    return new PhotoDataResponse
                    {
                        Success = this.Context.SaveChanges() > 0
                    };
                }
            }

            return new PhotoDataResponse
            {
                Success = false
            };
        }

        /// <summary>
        /// The delete favorit.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        public PhotoDataResponse DeletePhotoFromRoom(DeletePhotoFromRoomRequest deletePhotoFromRoomRequest)
        {
            if (deletePhotoFromRoomRequest == null ||
                deletePhotoFromRoomRequest.RoomId == Guid.Empty ||
                deletePhotoFromRoomRequest.PhotoId == Guid.Empty)
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            var existRoom = this.Context.Rooms.FirstOrDefault(x => x.Id == deletePhotoFromRoomRequest.RoomId);
            if (existRoom != null)
            {
                var existPhoto = existRoom.PhotoList.FirstOrDefault(x => x.Id == deletePhotoFromRoomRequest.PhotoId);
                if (existPhoto != null)
                {
                    this.Context.Photos.Remove(existPhoto);

                    return new PhotoDataResponse
                    {
                        Success = this.Context.SaveChanges() > 0
                    };
                }
            }

            return new PhotoDataResponse
            {
                Success = false
            };
        }

        /// <summary>
        /// AddPhoto
        /// </summary>
        /// <param name="photoDataRequest">
        /// </param>
        /// <returns>
        /// The <see cref="PhotoDataResponse"/>.
        /// </returns>
        public PhotoDataResponse AddPhotoToRoom(PhotoDataRequest photoDataRequest)
        {
            if (photoDataRequest == null ||
                photoDataRequest.RoomId == Guid.Empty ||
                string.IsNullOrEmpty(photoDataRequest.Image))
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            var existRoom = this.CurrentUser.Rooms.FirstOrDefault(x => x.Id == photoDataRequest.RoomId);
            if (existRoom == null)
            {
                return new PhotoDataResponse
                {
                    Success = false
                };
            }

            Guid imageId = Guid.Parse(Path.GetFileNameWithoutExtension(photoDataRequest.Image));

            var newPhoto = this.Context.Photos.Create();
            newPhoto.Id = imageId;
            newPhoto.IsMain = photoDataRequest.isMain;
            newPhoto.Image = photoDataRequest.Image;
            newPhoto.Time = DateTime.Now;

            existRoom.PhotoList.Add(newPhoto);

            return new PhotoDataResponse
            {
                Success = this.Context.SaveChanges() > 0,
                Photo = new PhotoData
                {
                    Id = imageId,
                    Image = photoDataRequest.Image
                }
            };
        }

        /// <summary>
        /// AddUserToRole
        /// </summary>
        /// <param name="userName">
        /// </param>
        /// <param name="roleName">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int AddUserToRole(string userName, string roleName)
        {
            var roleType = this.Context.RoleType.FirstOrDefault(x => x.Name == roleName);
            var user = this.Context.Users.FirstOrDefault(x => x.Name == userName);

            var userRole = this.Context.UserRole.Create();
            userRole.RoleType = roleType;
            userRole.User = user;

            this.Context.UserRole.Add(userRole);

            return this.Context.SaveChanges();
        }

        /// <summary>
        /// CreateBooking
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <param name="roomId">
        /// </param>
        /// <returns>
        /// The <see cref="BookingData"/>.
        /// </returns>
        public BookingJoinRoomData CreateBooking(BookingRequest request)
        {
            if (request == null || request.Tables == null || !request.Tables.Any())
            {
                return null;
            }

            var booking = this.Context.Bookings.Create();
            booking.Time = request.Time;
            booking.User = this.CurrentUser;
            booking.CreateTime = DateTime.Now;
            booking.PeopleCount = request.PeopleCount;

            if (this.CurrentUser.UserRoleList.Any(m => m.RoleType.Name == Role.BarOwner || m.RoleType.Name == Role.Administrator))
            {
                booking.State = (int)BookingStateEnum.BarOwnerAccepted;
            }
            else
            {
                booking.State = (int)BookingStateEnum.Waiting;
            }

            foreach (Guid tableId in request.Tables)
            {
                var bookingRoomTable = this.Context.BookingRoomTables.Create();
                bookingRoomTable.RoomTable = this.Context.RoomTables.FirstOrDefault(x => x.Id == tableId);

                booking.Tables.Add(bookingRoomTable);
            }

            this.Context.Bookings.Add(booking);
            this.Context.SaveChanges();

            return booking.CopyProperties();
        }

        /// <summary>
        /// The create conversation.
        /// </summary>
        /// <param name="resipientId">
        /// The resipient id.
        /// </param>
        /// <returns>
        /// The <see cref="ConversationsData"/>.
        /// </returns>
        public ConversationsData CreateConversation(Guid resipientId)
        {
            var conversations = this.Context.Conversations.Create();
            conversations.Time = DateTime.Now;
            conversations.User1 = this.CurrentUser;
            conversations.User2 = this.Context.Users.FirstOrDefault(x => x.Id == resipientId);
            conversations.State = 0;
            this.Context.Conversations.Add(conversations);
            this.Context.SaveChanges();

            var conversationData = new ConversationsData();
            conversationData.UserId = resipientId;
            conversationData.UserName = conversations.User2.Name;
            conversationData.ConversationId = conversations.Id;

            return conversationData;
        }

        /// <summary>
        /// CreateRegisterUser
        /// </summary>
        /// <param name="userData">
        /// </param>
        /// <returns>
        /// The <see cref="ResponseBase"/>.
        /// </returns>
        public ResponseBase CreateRegisterUser(UserData userData)
        {
            try
            {
                var user = this.Context.Users.Create();

                user.CopyProperties(userData);

                this.Context.Users.Add(user);

                return new ResponseBase { Success = this.Context.SaveChanges() > 0, };
            }
            catch (DbEntityValidationException e)
            {
                Logger.Log(e);

                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// The delete conversation message.
        /// </summary>
        /// <param name="conversationMessageId">
        /// The conversation message id.
        /// </param>
        public void DeleteConversationMessage(Guid conversationMessageId)
        {
            var message = this.Context.ConversationMessages.FirstOrDefault(x => x.Id == conversationMessageId);
            if (message != null)
            {
                this.Context.ConversationMessages.Remove(message);
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        /// The delete favorit.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        public void DeleteFavorit(Guid roomId)
        {
            var favorit = this.Context.Favorits.FirstOrDefault(x => x.Room.Id == roomId && x.User.Id == this.CurrentUser.Id);
            if (favorit != null)
            {
                this.Context.Favorits.Remove(favorit);
                this.Context.SaveChanges();
            }
        }

        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Context.Dispose();
        }


        public bool SetPosition(DevicePosition position)
        {
            bool isNew = false;
            var positionEntity = this.Context.Positions.FirstOrDefault(x => x.Room.Id == position.RoomId && x.Device.DeviceId == position.DeviceId);
            if (positionEntity == null)
            {
                positionEntity = this.Context.Positions.Create();
                positionEntity.Device = this.Context.Devices.FirstOrDefault(x => x.DeviceId == position.DeviceId);
                positionEntity.Room = this.Context.Rooms.FirstOrDefault(x => x.Id == position.RoomId);
                this.Context.Positions.Add(positionEntity);
                isNew = true;
            }
            positionEntity.X = position.X;
            positionEntity.Y = position.Y;

            positionEntity.Time = DateTime.Now;

            this.Context.SaveChanges();
            return isNew;
        }

        public List<Actor> GetActorsLeavingLocale()
        {
            var time = DateTime.Now.AddMinutes(-1);
            var toDelete = this.Context.Positions.Where(x => x.Time < time).ToList();
            List<Actor> actors = toDelete.Where(p => p.Device.User != null).Select(p => new Actor() {UserId = p.Device.User.Id ,UserName = p.Device.User.Name , Position = new DevicePosition() { DeviceId = p.Device.DeviceId ,RoomId = p.Room.Id }}).ToList();

            this.Context.Positions.RemoveRange(toDelete);
            this.Context.SaveChanges();

            return actors;
        }

        public void RemovePosition(DevicePosition position)
        {
            var positionEntity = this.Context.Positions.Where(x => x.Room.Id == position.RoomId && x.Device.DeviceId == position.DeviceId);
            if (positionEntity.Any())
            {
                this.Context.Positions.RemoveRange(positionEntity);
            }

            this.Context.SaveChanges();
        }

        /// <summary>
        /// The get actor by device id.
        /// </summary>
        /// <param name="deviceId">
        /// The device id.
        /// </param>
        /// <returns>
        /// The <see cref="Actor"/>.
        /// </returns>
        public Actor GetActorByDeviceId(string deviceId)
        {
            
            try
            {
                using (var context = new LpsContext())
                {
                    var firstOrDefault = context.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
                    var user = firstOrDefault?.User;
                    if (user != null)
                    {
                        var actor = new Actor();
                        actor.UserId = user.Id;
                        actor.UserName = user.Name;
                        actor.PhotoPath =
                            user.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault()
                            ?? string.Empty;
                        return actor;
                    }
                    return null;
                }
            
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        /// <summary>
        /// GetBookingListByUser
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<BookingJoinRoomData> GetBookingHistoryByUser(Guid? roomId)
        {
            IList<BookingJoinRoomData> bookingJoinRoomDataList;
            if (roomId == null)
            {
                bookingJoinRoomDataList = this.CurrentUser.Bookings.Select(m => m.CopyProperties()).ToList();
            }
            else
            {
                bookingJoinRoomDataList = this.CurrentUser.Bookings.Where(x => x.Tables.Any(t => t.RoomTable.Room.Id == roomId)).Select(m => m.CopyProperties()).ToList();
            }

            return bookingJoinRoomDataList;
        }

        /// <summary>
        /// GetBookingHistoryByUser
        /// </summary>
        /// <param name="roomId">
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<BookingJoinRoomData> GetBookingHistoryForKassaByRoom(Guid roomId)
        {
            // check authorization
            Room room = this.CurrentUser.Rooms.FirstOrDefault(m => m.Id == roomId);
            if (room == null)
            {
                return null;
            }

            //IList<BookingJoinRoomData> bookingJoinRoomDataList = 
            //    room.Tables.SelectMany(m => m.Bookings).Select(m => m.CopyProperties()).ToList();

            IList<BookingJoinRoomData> bookingJoinRoomDataList =
                       this.Context.Bookings.Where(x => x.Tables.Any(m => m.RoomTable.Room.Id == room.Id))
                                            .Where(x => x.Time > DateTime.Now || x.State == (int)BookingStateEnum.Waiting)
                                            .ToList()
                                            .Select(m => m.CopyProperties())
                                            .ToList();

            return bookingJoinRoomDataList;
        }

        

        /// <summary>
        /// The get actors by map id.
        /// </summary>
        /// <param name="roomId">
        /// The room Id.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public List<Actor> GetActorsModel(Guid roomId)
        {
            var model = new List<Actor>();
            try
            {
                using (var context = new LpsContext())
                {
                    var actors = context.Positions.Where(x => x.Room.Id == roomId && x.Device.User != null)
                            .Select(
                                x =>
                                new Actor()
                                {
                                    Position = new DevicePosition()
                                    {
                                        DeviceId = x.Device.DeviceId,
                                        RoomId = x.Room.Id,
                                        X = x.X,
                                        Y = x.Y
                                    },
                                    UserId = x.Device.User.Id,
                                    UserName = x.Device.User.Name,
                                    PhotoPath = x.Device.User.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault() ?? string.Empty

                                }).ToList();

                    model.AddRange(actors);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return model;
        }


        /// <summary>
        /// The get cities.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<string> GetCities()
        {
            return this.Context.Rooms.Select(x => x.City).Distinct().ToList();
        }

        public object GetLocaleNames()
        {
            return this.Context.Rooms.Select(x => new { Value = x.Id, Text = x.Name }).ToList();
        }

        /// <summary>
        /// The get conversation.
        /// </summary>
        /// <param name="resipientId">
        /// The resipient id.
        /// </param>
        /// <returns>
        /// The <see cref="ConversationsData"/>.
        /// </returns>
        public ConversationsData GetConversation(Guid resipientId)
        {
            try
            {
                var currentUserId = this.CurrentUser.Id;
                var conversation =
                    this.Context.Conversations.FirstOrDefault(
                        x =>
                        (x.User1.Id == currentUserId && x.User2.Id == resipientId)
                        || (x.User2.Id == currentUserId && x.User1.Id == resipientId));

                if (conversation == null)
                {
                    return this.CreateConversation(resipientId);
                }

                var resipient = this.Context.Users.FirstOrDefault(x => x.Id == resipientId);
                var conversationData = new ConversationsData();
                conversationData.ConversationId = conversation.Id;
                conversationData.UserId = resipient.Id;
                conversationData.UserName = resipient.Name;
                foreach (var message in conversation.Messages)
                {
                    conversationData.Messages.Add(
                        new ChatMessage()
                        {
                            ConversationId = conversation.Id,
                            ConversationMessageId = message.Id,
                            Message = message.Message,
                            IsMe = message.Sender.Id == this.CurrentUser.Id
                        });
                }

                return conversationData;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

            return null;
        }

        /// <summary>
        /// The get conversations.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ConversationsData> GetConversations(Guid? roomId)
        {
            var myConversations =
                this.CurrentUser.MyConversations.Select(
                    x => new ConversationsData() { ConversationId = x.Id, UserId = x.User2_Id, UserName = x.User2.Name })
                    .ToList();
            var othersConversations =
                this.CurrentUser.TheirsConversations.Select(
                    x => new ConversationsData() { ConversationId = x.Id, UserId = x.User1_Id, UserName = x.User1.Name })
                    .ToList();
            myConversations.AddRange(othersConversations);

            return myConversations;
        }

        /// <summary>
        /// The get favorit names.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public object GetFavoritNames()
        {
            return
                this.Context.Favorits.Where(x => x.User.Id == this.CurrentUser.Id)
                    .Select(x => new { Value = x.Room.Id, Text = x.Room.Name })
                    .ToList();
        }

        /// <summary>
        /// GetKitchenInternationalTypes
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<KitchenInternationalTypeData> GetKitchenInternationalTypes()
        {
            IList<KitchenInternationalTypeData> kitchenMenuInternationalTypeDataList =
                this.Context.KitchenInternationalType.ToList().Select(x => x.CopyProperties()).ToList();

            return kitchenMenuInternationalTypeDataList;
        }

        /// <summary>
        /// GetKitchenInternationalTypes
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<SpecializationTypeData> GetSpecializationTypes()
        {
            IList<SpecializationTypeData> specializationTypeDataList =
                this.Context.SpecializationTypes.ToList().Select(x => x.CopyProperties()).ToList();

            return specializationTypeDataList;
        }

        /// <summary>
        /// GetKitchenMenuTypes
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<KitchenMenuTypeData> GetKitchenMenuTypes()
        {
            IList<KitchenMenuTypeData> kitchenMenuTypeDataList =
                this.Context.KitchenMenuTypes.ToList().Select(x => x.CopyProperties()).ToList();

            return kitchenMenuTypeDataList;
        }

        /// <summary>
        /// GetKitchenTypes
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<KitchenTypeData> GetKitchenTypes()
        {
            IList<KitchenTypeData> kitchenTypeDataList =
                this.Context.KitchenType.ToList().Select(x => x.CopyProperties()).ToList();

            return kitchenTypeDataList;
        }

        /// <summary>
        /// GetLocations
        /// </summary>
        /// <param name="requestLocationData">
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<RoomData> GetLocations(RequestLocationData requestLocationData)
        {
            if (requestLocationData == null)
            {
                return null;
            }

            try
            {
                var location = Help.CreatePoint(requestLocationData.Latitude, requestLocationData.Longitude);
                double radius = Help.ConvertMilesToMeters(requestLocationData.Radius);

                IList<RoomData> locationDataList =
                    this.Context.Rooms.AsNoTracking()
                        .Where(
                            m =>
                            requestLocationData.IsChatExist == false || m.IsChatExist == requestLocationData.IsChatExist)
                        .Where(
                            m =>
                            string.IsNullOrEmpty(requestLocationData.LocationName)
                            || m.Name.Contains(requestLocationData.LocationName))
                        .Where(
                            m =>
                            requestLocationData.KitchenTypes.Count == 0
                            || requestLocationData.KitchenTypes.All(
                                i => m.RoomKitchenList.Select(k => k.KitchenType.Id).Any(k => k == i)))
                        .Where(
                            m =>
                            requestLocationData.KitchenInternationalTypes.Count == 0
                            || requestLocationData.KitchenInternationalTypes.All(
                                i =>
                                m.RoomKitchenInternationalList.Select(k => k.KitchenInternationalType.Id)
                                    .Any(k => k == i)))
                        .Where(m => m.Location.Distance(location) <= radius)
                        .ToList()
                        .Select(x => x.CopyProperties(this.CurrentUser))
                        .ToList();

                return locationDataList;
            }
            catch (DbEntityValidationException e)
            {
                Logger.Log(e);

                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// GetRoomById
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// The <see cref="RoomData"/>.
        /// </returns>
        public RoomData GetRoomById(Guid id)
        {
            Room existRoom = this.Context.Rooms.FirstOrDefault(m => m.Id == id);
            if (existRoom != null)
            {
                return existRoom.CopyProperties(this.CurrentUser);
            }

            return null;
        }

        /// <summary>
        ///     The get rooms.
        /// </summary>
        /// <returns>
        ///     The <see cref="IList" />.
        /// </returns>
        public List<RoomInfo> GetRoomInfos()
        {
            var currentUserId = Guid.Empty;
            if (this.CurrentUser != null)
            {
                currentUserId = this.CurrentUser.Id;
            }

            var rooms = new List<RoomInfo>();
            rooms.AddRange(
                this.Context.Rooms.Select(
                    x =>
                    new RoomInfo
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Lat = x.Latitude,
                        Lng = x.Longitude,
                        ImageFileName = x.CanvasImage,
                        IsChatExist = x.IsChatExist,
                        City = x.City,
                        IsFavorite = currentUserId != Guid.Empty && x.FavoritsList.Any(y => y.User.Id == currentUserId),
                        Rating = x.RatingList.Average(y => (double?)y.State) ?? 0.0
                    }).ToList());
            return rooms;
        }

        /// <summary>
        /// GetFavorits
        /// </summary>
        /// <returns></returns>
        public List<RoomInfo> GetFavorits()
        {
            return this.Context.Favorits.Where(x => x.User.Id == this.CurrentUser.Id).Select(
                    x =>
                    new RoomInfo
                    {
                        Id = x.Room.Id,
                        Name = x.Room.Name,
                        Lat = x.Room.Latitude,
                        Lng = x.Room.Longitude,
                        ImageFileName = x.Room.PhotoList.Where(m => m.IsMain == true).Select(m => m.Image).FirstOrDefault() ?? string.Empty,
                        IsChatExist = x.Room.IsChatExist,
                        City = x.Room.City,
                        IsFavorite = true,
                        Rating = x.Room.RatingList.Average(y => (double?)y.State) ?? 0.0

                    }).ToList();

        }

        /// <summary>
        /// The get room infos.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<RoomInfo> GetRoomInfos(RequestLocationData request)
        {
            var location = Help.CreatePoint(request.Latitude, request.Longitude);
            double radius = Help.ConvertMilesToMeters(request.Radius);

            var resultList =
                this.Context.Rooms.Where(
                    m =>
                    request.KitchenTypes.Count == 0
                    || request.KitchenTypes.All(i => m.RoomKitchenList.Select(k => k.KitchenType.Id).Any(k => k == i)))
                    .Where(m => m.Location.Distance(location) <= radius)
                    .Select(
                        x =>
                        new RoomInfo
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Lat = x.Latitude,
                            Lng = x.Longitude,
                            ImageFileName = x.CanvasImage,
                            IsChatExist = x.IsChatExist,
                            City = x.City,
                            IsFavorite = x.FavoritsList.Any(y => y.User.Id == this.CurrentUser.Id),
                            Rating = x.RatingList.Average(y => (double?)y.State) ?? 0.0
                        })
                    .ToList();

            return resultList;
        }

        /// <summary>
        /// GetRoomListByUser
        /// </summary>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public IList<RoomData> GetRoomListByUser()
        {
            IList<RoomData> locationDataList = this.CurrentUser.Rooms.Select(x => x.CopyProperties(this.CurrentUser)).ToList();

            return locationDataList;
        }

        /// <summary>
        /// GetTableModel
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        public object GetTableModel(Guid id)
        {
            var room = this.Context.Rooms.FirstOrDefault(m => m.Id == id);
            var model = new
            {
                height = room.RoomHeight,
                wight = room.RoomWidth,
                backgroungImage = room.BackgroungImage,
                tables = room.Tables.Select(x => new { id = x.Id, angle = x.Angle })
            };

            return model;
        }


        public RoomModel GetJsonRoomModel(Guid id)
        {
            var room = this.Context.Rooms.FirstOrDefault(x => x.Id == id);
            var model = new RoomModel() { Id = room.Id, Json = room.JsonModel, Zoom = room.LayoutZoom, RealScaleFactor = room.RealScaleFactor };
            return model;
        }

        public RoomModel GetSvgRoomModel(Guid id)
        {
            var room = this.Context.Rooms.FirstOrDefault(x => x.Id == id);
            var model = new RoomModel() { Id = room.Id, SvgLayout = room.SvgLayout, TablesLayout = room.TableLayout };
            return model;
        }

        public void SaveJsonRoomModel(RoomModel jsonModel)
        {
            var room = this.Context.Rooms.FirstOrDefault(x => x.Id == jsonModel.Id);
            room.JsonModel = jsonModel.Json;
            room.LayoutZoom = jsonModel.Zoom;
            room.SvgLayout = jsonModel.SvgLayout;
            room.TableLayout = jsonModel.TablesLayout;
            room.RealScaleFactor = jsonModel.RealScaleFactor;
            room.BackgroungImage = jsonModel.BackgroundImage;
            room.RoomWidth = jsonModel.Width;
            room.RoomHeight = jsonModel.Height;


            List<Guid> tableIds = jsonModel.Tables.Select(x => x.ItemId).ToList();
            var toDelete = room.Tables.Where(x => !tableIds.Contains(x.Id)).ToList();
            foreach (var table in jsonModel.Tables)
            {
                var entity = room.Tables.FirstOrDefault(x => x.Id == table.ItemId);
                if (entity == null)
                {
                    entity = new RoomTable()
                    {
                        Id = table.ItemId,
                        Angle = table.Angle,
                        X = table.Left,
                        Y = table.Top,
                        Description = table.Description,
                        Type = table.ItemType,
                        Width = table.Width,
                        Height = table.Height,
                        Room = room
                    };
                    this.Context.RoomTables.Add(entity);
                    room.Tables.Add(entity);
                }
                else
                {
                    entity.Angle = table.Angle;
                    entity.X = table.Left;
                    entity.Y = table.Top;
                    entity.Height = table.Height;
                    entity.Width = table.Width;
                    entity.Description = table.Description;
                }
            }

            foreach (var table in toDelete)
            {
                this.Context.RoomTables.Remove(table);
            }


            List<Guid> beaconIds = jsonModel.Beacons.Select(x => x.ItemId).ToList();
            var toDeleteBeacons = room.BeaconList.Where(x => !beaconIds.Contains(x.Id)).ToList();
            foreach (var beacon in jsonModel.Beacons)
            {
                var entity = room.BeaconList.FirstOrDefault(x => x.Id == beacon.ItemId);
                if (entity == null)
                {
                    entity = new LpsServer.Data.Entities.Beacon()
                    {
                        Id = beacon.ItemId,
                        X = beacon.Left,
                        Y = beacon.Top,
                        Identifier1 = jsonModel.Id,
                        Identifier2 = 1,
                        Identifier3 = string.IsNullOrEmpty(beacon.Id3) ? 0 : Convert.ToInt32(beacon.Id3),
                        Room = room
                    };
                    this.Context.Beacons.Add(entity);
                    room.BeaconList.Add(entity);
                }
                else
                {
                    entity.X = beacon.Left;
                    entity.Y = beacon.Top;
                    entity.Identifier3 = string.IsNullOrEmpty(beacon.Id3) ? 0 : Convert.ToInt32(beacon.Id3);
                }
            }

            foreach (var beacon in toDeleteBeacons)
            {
                this.Context.Beacons.Remove(beacon);
            }
            this.Context.SaveChanges();
        }



        /// <summary>
        /// The get user by name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public UserData GetUserData(string userName)
        {
            try
            {
                var user = this.Context.Users.FirstOrDefault(m => m.Name == userName);
                if (user != null)
                {
                    var userData = user.CopyProperties();

                    return userData;
                }

                return null;

            }
            catch (Exception err)
            {
                int i = 0;
            }
            //catch (DbEntityValidationException e)
            //{
            //    Logger.Log(e);

            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Debug.WriteLine(
            //            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name,
            //            eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}

            return null;
        }

        /// <summary>
        /// GetUserProfile
        /// </summary>
        /// <returns>
        /// The <see cref="UserData"/>.
        /// </returns>
        public UserData GetUserProfile()
        {
            return this.CurrentUser.CopyProperties();
        }

        /// <summary>
        /// The insert favorit.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        public bool InsertFavorit(Guid roomId)
        {
            var favorit = this.Context.Favorits.Create();
            favorit.User = this.CurrentUser;
            favorit.Room = this.Context.Rooms.FirstOrDefault(x => x.Id == roomId);

            this.Context.Favorits.Add(favorit);

            return this.Context.SaveChanges() > 0;
        }

        /// <summary>
        /// The register user device.
        /// </summary>
        /// <param name="userName">
        /// The user Name.
        /// </param>
        /// <param name="deviceId">
        /// The device Id.
        /// </param>
        public void RegisterUserCurrentDevice(string userName, string deviceId)
        {
            var device = this.Context.Devices.FirstOrDefault(x => x.DeviceId == deviceId);
            if (device == null)
            {
                device = new Device() { DeviceId = deviceId };
                this.Context.Devices.Add(device);

            }
            var user = this.Context.Users.FirstOrDefault(x => x.Name == userName);
            device.User = user;
            if (user != null)
            {
                user.CurrentDevice = device;
            }
            this.Context.SaveChanges();
        }

        /// <summary>
        /// The save conversation message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string SaveConversationMessage(ChatMessage message)
        {
            var conversation = this.Context.Conversations.FirstOrDefault(x => x.Id == message.ConversationId);
            if (conversation != null)
            {
                var conversationMessage = this.Context.ConversationMessages.Create();
                conversationMessage.Message = message.Message;
                conversationMessage.Sender = this.CurrentUser;
                conversationMessage.Time = message.Time;

                conversation.Messages.Add(conversationMessage);
                this.Context.SaveChanges();

                message.ConversationMessageId = conversationMessage.Id;
                return this.CurrentUser.Id == conversation.User1_Id ? conversation.User2.Name : conversation.User1.Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// The save position.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SavePosition(Position position)
        {
            var ctx = this.Context;
            ctx.Positions.Add(position);

            return ctx.SaveChanges() > 0;
        }

        /// <summary>
        /// SaveRating
        /// </summary>
        /// <param name="ratingData">
        /// </param>
        /// <returns>
        /// The <see cref="ResponseSaveRating"/>.
        /// </returns>
        public ResponseSaveRating SaveRating(RatingData ratingData)
        {
            if (ratingData == null)
            {
                return new ResponseSaveRating { Success = false };
            }

            try
            {
                Room existingRoom = this.Context.Rooms.Find(ratingData.RoomId);
                if (existingRoom != null)
                {
                    var newRating = this.Context.Ratings.Create();
                    newRating.CopyProperties(ratingData, this.CurrentUser);

                    existingRoom.RatingList.Add(newRating);
                }

                return new ResponseSaveRating { Success = this.Context.SaveChanges() > 0, };
            }
            catch (DbEntityValidationException e)
            {
                Logger.Log(e);

                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }

        /// <summary>
        /// SaveRoom
        /// </summary>
        /// <param name="roomData">
        /// </param>
        /// <returns>
        /// The <see cref="ResponseSaveRoom"/>.
        /// </returns>
        public ResponseSaveRoom SaveRoom(RoomData roomData)
        {
            if (roomData == null)
            {
                return new ResponseSaveRoom { Success = false };
            }

            try
            {
                Room newRoom = this.CurrentUser.Rooms.FirstOrDefault(x => x.Id == roomData.Id);
                if (newRoom != null)
                {
                    newRoom.CopyProperties(roomData, this.CurrentUser);
                }
                else
                {
                    var beaconRoomId = new Guid("75afdfe0-7413-e611-9523-5cf37069f81b");
                    var firstRoom = this.Context.Rooms.Count(x => x.Id.Equals(beaconRoomId)) == 0;
                    newRoom = this.Context.Rooms.Create();
                    newRoom.CopyProperties(roomData, this.CurrentUser);

                    if (firstRoom)
                    {
                        newRoom.Id = beaconRoomId;
                    }
                    else
                    {
                        newRoom.Id = Guid.NewGuid();
                    }

                    this.Context.Rooms.Add(newRoom);
                }

                // KitchenMenu
                newRoom.KitchenMenuList.ToList().ForEach(k => this.Context.KitchenMenus.Remove(k));
                foreach (var kitchenMenus in roomData.KitchenMenus)
                {
                    KitchenMenuType existing = this.Context.KitchenMenuTypes.Find(kitchenMenus.KitchenMenuTypeId);
                    if (existing != null)
                    {
                        var kitchenMenu = this.Context.KitchenMenus.Create();
                        kitchenMenus.CopyProperties(kitchenMenu, existing);

                        newRoom.KitchenMenuList.Add(kitchenMenu);
                    }
                }

                // BusinessHours
                newRoom.BusinessHoursList.ToList().ForEach(k => this.Context.BusinessHours.Remove(k));
                roomData.BusinessHours.Select(x => x.CopyProperties(this.Context.BusinessHours.Create()))
                    .ToList()
                    .ForEach(k => newRoom.BusinessHoursList.Add(k));

                // RoomKitchen
                newRoom.RoomKitchenList.ToList().ForEach(k => this.Context.RoomKitchen.Remove(k));
                foreach (var kitchenTypes in roomData.KitchenTypes)
                {
                    KitchenType existing = this.Context.KitchenType.Find(kitchenTypes);
                    if (existing != null)
                    {
                        var roomKitchen = this.Context.RoomKitchen.Create();
                        roomKitchen.KitchenType = existing;

                        newRoom.RoomKitchenList.Add(roomKitchen);
                    }
                }

                // Specialization
                newRoom.SpecializationList.ToList().ForEach(k => this.Context.Specializations.Remove(k));
                foreach (var specializationTypes in roomData.SpecializationTypes)
                {
                    SpecializationType existing = this.Context.SpecializationTypes.Find(specializationTypes.Id);
                    if (existing != null)
                    {
                        var specialization = this.Context.Specializations.Create();
                        specialization.SpecializationType = existing;

                        newRoom.SpecializationList.Add(specialization);
                    }
                }

                // RoomInternationalKitchen
                newRoom.RoomKitchenInternationalList.ToList()
                    .ForEach(k => this.Context.RoomKitchenInternational.Remove(k));
                foreach (var kitchenInternationalType in roomData.KitchenInternationalTypes)
                {
                    KitchenInternationalType existing =
                        this.Context.KitchenInternationalType.Find(kitchenInternationalType);
                    if (existing != null)
                    {
                        var roomKitchenInternational = this.Context.RoomKitchenInternational.Create();
                        roomKitchenInternational.KitchenInternationalType = existing;

                        newRoom.RoomKitchenInternationalList.Add(roomKitchenInternational);
                    }
                }

                var success = this.Context.SaveChanges() > 0;
                return new ResponseSaveRoom { Success = success, RoomId = newRoom.Id };
            }
            catch (DbEntityValidationException e)
            {
                Logger.Log(e);

                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }

        ///// <summary>
        ///// The save room.
        ///// </summary>
        ///// <param name="setRoomData">
        ///// The set Room Data.
        ///// </param>
        ///// <returns>
        ///// The <see cref="bool"/>.
        ///// </returns>
        //public ResponseSaveRoom SaveRoomItem(RequestRoomData setRoomData)
        //{
        //    try
        //    {
        //        ModelFactory modelFactory = new ModelFactory(this);
        //        Room room = modelFactory.ParseToRoom(setRoomData);

        //        Room existRoom = this.CurrentUser.Rooms.FirstOrDefault(m => m.Id == room.Id);
        //        if (existRoom == null)
        //        {
        //            return new ResponseSaveRoom { Success = false };
        //        }

        //        existRoom.Height = setRoomData.RoomHeight;
        //        existRoom.CanvasImage = setRoomData.CanvasImage;
        //        existRoom.RoomHeight = room.RoomHeight;
        //        existRoom.RoomWidth = room.RoomWidth;

        //        // CornerPoint   
        //        var deletedCornerPointList = existRoom.CornerPointList.ToList();
        //        foreach (var cornerPoint in deletedCornerPointList)
        //        {
        //            this.Context.CornerPoints.Remove(cornerPoint);
        //        }

        //        foreach (var cornerPoint in room.CornerPointList)
        //        {
        //            existRoom.CornerPointList.Add(cornerPoint);
        //        }

        //        // RoomItem
        //        // http://www.entityframeworktutorial.net/EntityFramework4.3/update-one-to-many-entity-using-dbcontext.aspx
        //        var coomItemComparer = new RoomItemComparer();
        //        var addRoomItems = room.RoomItemList.Except(existRoom.RoomItemList, coomItemComparer).ToList();
        //        var deletedRoomItems = existRoom.RoomItemList.Except(room.RoomItemList, coomItemComparer).ToList();
        //        var updatedRoomItems = room.RoomItemList.Except(addRoomItems, coomItemComparer).ToList();
        //        foreach (var roomItem in updatedRoomItems)
        //        {
        //            RoomItem existing = this.Context.RoomItems.Find(roomItem.Id);
        //            if (existing != null)
        //            {
        //                existing.CopyProperties(roomItem);
        //            }
        //        }

        //        deletedRoomItems.ToList().ForEach(table => this.Context.RoomItems.Remove(table));
        //        addRoomItems.ToList().ForEach(table => existRoom.RoomItemList.Add(table));

        //        // RoomTable
        //        var roomTableComparer = new RoomTableComparer();
        //        var addRoomTables = room.Tables.Except(existRoom.Tables, roomTableComparer).ToList();
        //        var deletedRoomTables = existRoom.Tables.Except(room.Tables, roomTableComparer).ToList();
        //        var updatedRoomTables = room.Tables.Except(addRoomTables, roomTableComparer).ToList();
        //        foreach (var table in updatedRoomTables)
        //        {
        //            RoomTable existing = this.Context.RoomTables.Find(table.Id);
        //            if (existing != null)
        //            {
        //                existing.CopyProperties(table);
        //            }
        //        }

        //        deletedRoomTables.ToList().ForEach(table => this.Context.RoomTables.Remove(table));
        //        addRoomTables.ToList().ForEach(table => existRoom.Tables.Add(table));

        //        // Beacon
        //        var beaconComparer = new BeaconComparer();
        //        var addBeacons = room.BeaconList.Except(existRoom.BeaconList, beaconComparer).ToList();
        //        var deletedBeacons = existRoom.BeaconList.Except(room.BeaconList, beaconComparer).ToList();
        //        var updatedBeacons = room.BeaconList.Except(addBeacons, beaconComparer).ToList();
        //        foreach (var beacon in updatedBeacons)
        //        {
        //            Beacon existing = this.Context.Beacons.Find(beacon.Id);
        //            if (existing != null)
        //            {
        //                existing.CopyProperties(beacon);
        //            }
        //        }

        //        deletedBeacons.ToList().ForEach(table => this.Context.Beacons.Remove(table));
        //        addBeacons.ToList().ForEach(table => existRoom.BeaconList.Add(table));

        //        return new ResponseSaveRoom { Success = this.Context.SaveChanges() > 0, RoomId = room.Id };
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        Logger.Log(e);

        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Debug.WriteLine(
        //                "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", 
        //                eve.Entry.Entity.GetType().Name, 
        //                eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }

        //        throw;
        //    }
        //}

        /// <summary>
        /// SaveUserProfile
        /// </summary>
        /// <param name="userData">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SaveUserProfile(UserData userData)
        {
            var user = this.CurrentUser;
            user.Name = userData.UserName;
            user.Password = userData.Password;
            user.Email = userData.Email;

            return this.Context.SaveChanges() > 0;
        }

        /// <summary>
        /// UpdateBooking
        /// </summary>
        /// <param name="response">
        /// </param>
        /// <returns>
        /// The <see cref="BookingJoinRoomData"/>.
        /// </returns>
        public BookingJoinRoomData UpdateBooking(BookingResponse response)
        {
            try
            {
                var booking = this.Context.Bookings.FirstOrDefault(x => x.Id == response.BookingId);

                // user                    
                if (booking.User.Id == this.CurrentUser.Id)
                {
                    booking.State = (int)BookingStateEnum.Canceled;
                }
                // kassa                    
                else
                {
                    booking.State = response.Accepted ? (int)BookingStateEnum.Accepted : (int)BookingStateEnum.Rejected;
                }

                this.Context.SaveChanges();

                return booking.CopyProperties();
            }
            catch (DbEntityValidationException e)
            {
                Logger.Log(e);

                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw;
            }
        }



        public IList<TypeaheadLocationData> GetTypeaheadLocation(string query)
        {
            var e = this.Context.Rooms.Where(x => x.Name == "e" || x.City == "c").Expression;
            //if(string.IsNullOrEmpty(query))
            //{
            //    return null;
            //}

            var searchQuery = this.Context.Rooms
                                          .AsNoTracking()
                                          .Where(x => string.IsNullOrEmpty(query) || x.Name.Contains(query))
                                          .OrderBy(n => n.Name)
                                          .ToList()
                                          .Select(m => m.CopyProperties())
                                          .ToList();

            return searchQuery;
        }

        public IList<TypeaheadCityData> GetTypeaheadCity(string query)
        {
            //if(string.IsNullOrEmpty(query))
            //{
            //    return null;
            //}

            var searchQuery = this.Context.Rooms
                                          .AsNoTracking()
                                          .Where(x => string.IsNullOrEmpty(query) || x.City.Contains(query))
                                          .GroupBy(m => m.City)
                                          .ToList()
                                          .Select(m => m.First().CopyProperties(true))
                                          .ToList();

            return searchQuery;
        }

        private static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
        }

        private IEnumerable<Timestamp> EachTime(DateTime from, DateTime to)
        {
            int step = 30;
            for (DateTime currentTime = from; currentTime.TimeOfDay < to.TimeOfDay; currentTime = currentTime.AddMinutes(step))
            {
                yield return new Timestamp
                {
                    date = string.Format("{0:s}", RoundUp(currentTime, TimeSpan.FromMinutes(step))),
                    dateTime = RoundUp(currentTime, TimeSpan.FromMinutes(step))
                };
            }
        }

        /// <summary>
        /// GetReservation 
        /// </summary>
        /// <returns></returns>
        public BookingMapData GetBookinMap(Reservation reservation)
        {
            if (reservation == null || reservation.RoomId == Guid.Empty)
            {
                return null;
            }

            var existRoom = this.Context.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);
            if (existRoom == null)
            {
                return null;
            }

            List<BusinessHours> list = existRoom.BusinessHoursList.ToList();
            var dayOfWeek = reservation.Time.DayOfWeek == DayOfWeek.Sunday ? 6 : ((int)reservation.Time.DayOfWeek - 1);
            BusinessHours dusinessHours = list.FirstOrDefault(m => m.Day == dayOfWeek);
            
            List<Timestamp> timestampList = new List<Timestamp>();
            if (dusinessHours.Close)
            {                
                timestampList.Add(new Timestamp
                {
                    date = new DateTime(DateTime.Now.Year, 1, 1).ToShortTimeString(),
                    dateTime = new DateTime(DateTime.Now.Year, 1, 1)
                });                
            } 
            else
            {
                if (dusinessHours.PauseStart.HasValue && dusinessHours.PauseEnd.HasValue)
                {
                    foreach (Timestamp day in EachTime(reservation.Time.Date.Add(dusinessHours.OpenTime.TimeOfDay), dusinessHours.PauseStart.Value))
                    {
                        timestampList.Add(day);
                    }
                    foreach (Timestamp day in EachTime(reservation.Time.Date.Add(dusinessHours.PauseEnd.Value.TimeOfDay), dusinessHours.CloseTime))
                    {
                        timestampList.Add(day);
                    }
                }
                else
                {
                    foreach (Timestamp day in EachTime(reservation.Time.Date.Add(dusinessHours.OpenTime.TimeOfDay), dusinessHours.CloseTime))
                    {
                        timestampList.Add(day);
                    }
                }
            }

            List<RoomTableJoinKassaData> boomTableJoinKassaDataList = 
                existRoom.Tables.Select(x => new RoomTableJoinKassaData(x.CopyProperties())).ToList();                

            foreach (RoomTableJoinKassaData boomTableJoinKassaData in boomTableJoinKassaDataList)
            {
                List<TimeStampJoinKassa> timeStampJoinKassaList = new List<TimeStampJoinKassa>();
                foreach (Timestamp timestamp in timestampList)
                {
                    var from = timestamp.dateTime.AddHours(-2);
                    var to = timestamp.dateTime;

                    var bookings = this.Context.Bookings.Where(x => x.Tables.Any(m => m.RoomTable.Id == boomTableJoinKassaData.Id))
                                                        .Where(x => x.Time <= to && x.Time > from)
                                                        .ToList();
                    if (bookings.Any())
                    {
                        var exist = timeStampJoinKassaList.FirstOrDefault(m => m.BookingData.BookingId == bookings.Last().Id);
                        if (exist != null)
                        {
                            exist.SubColumns.Add(new TimeStampJoinKassa(timestamp));                                
                        }
                        else
                        {
                            TimeStampJoinKassa timeStampJoinKassa = new TimeStampJoinKassa(timestamp);
                            timeStampJoinKassa.BookingData = bookings.Last().CopyProperties();
                            timeStampJoinKassa.SubColumns.Add(new TimeStampJoinKassa(timestamp));

                            timeStampJoinKassaList.Add(timeStampJoinKassa);
                        }
                    }
                    else
                    {
                        timeStampJoinKassaList.Add(new TimeStampJoinKassa(timestamp));
                    }
                }
                boomTableJoinKassaData.TimeStampJoinKassaList.AddRange(timeStampJoinKassaList);                    
            }
           
            var bookingMapData = new BookingMapData
            {
                RoomTableJoinKassaDataList = boomTableJoinKassaDataList,
                TimeStampList = timestampList
            };

            return bookingMapData;
        }

        /// <summary>
        /// GetReservation 
        /// </summary>
        /// <returns></returns>
        public ReservationData GetReservation(Reservation reservation)
        {
            if (reservation == null || reservation.RoomId == Guid.Empty)
            {
                return null;
            }

            var existRoom = this.Context.Rooms.FirstOrDefault(x => x.Id == reservation.RoomId);
            if (existRoom == null)
            {
                return null;
            }

            List<BusinessHours> list = existRoom.BusinessHoursList.ToList();
            var dayOfWeek = reservation.Time.DayOfWeek == DayOfWeek.Sunday ? 6 : ((int)reservation.Time.DayOfWeek - 1);
            BusinessHours dusinessHours = list.FirstOrDefault(m => m.Day == dayOfWeek);

            List<Timestamp> timestampList = new List<Timestamp>();
            if (!dusinessHours.Close)
            {
                if (DateTime.Now.Date == reservation.Time.Date)
                {
                    // todo
                    foreach (Timestamp day in EachTime(DateTime.Now.AddMinutes(30), dusinessHours.CloseTime.AddMinutes(-60)))
                    {
                        timestampList.Add(day);
                    }
                }
                else if (dusinessHours.PauseStart.HasValue && dusinessHours.PauseEnd.HasValue)
                {
                    foreach (Timestamp day in EachTime(dusinessHours.OpenTime, dusinessHours.PauseStart.Value))
                    {
                        timestampList.Add(day);
                    }
                    foreach (Timestamp day in EachTime(dusinessHours.PauseEnd.Value, dusinessHours.CloseTime.AddMinutes(-60)))
                    {
                        timestampList.Add(day);
                    }
                }
                else
                {
                    foreach (Timestamp day in EachTime(dusinessHours.OpenTime, dusinessHours.CloseTime.AddMinutes(-60)))
                    {
                        timestampList.Add(day);
                    }
                }
            }

            var reservationData = new ReservationData
            {
                capacities = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                reservation_allowed_advance = 360,
                selected_capacity = 2,
                selected_date = string.Format("{0:s}", DateTime.Now),
                //reservation_restricted_weekdays = new List<DateTime>(),
                //reservation_restricted_days = new List<DateTime>(),
                //reservation_allowed_days = new List<DateTime>(),
                times = new List<TimeObject>{
                        new TimeObject {
                            times = timestampList
                    }
                }
            };

            return reservationData;
        }


        public void SaveRoomBackgroungImage(Guid roomId, string image)
        {
            var room = this.Context.Rooms.FirstOrDefault(x => x.Id == roomId);
            room.BackgroungImage = image;
            this.Context.SaveChanges();
        }


        #endregion
    }
}