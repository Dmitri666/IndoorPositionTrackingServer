// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FillDatabase.cs" company="">
//   
// </copyright>
// <summary>
//   The fill database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;

    using Lps.Contracts.Helper;

    using LpsServer.Data.Entities;

    using Lps.Contracts.ViewModel.Rooms;

    /// <summary>
    /// The fill database.
    /// </summary>
    public class FillDatabase
    {
        #region RoleType

        /// <summary>
        /// The device data.
        /// </summary>
        private static readonly IList<RoleType> roleTypeData = new List<RoleType>
        {
            new RoleType
                {
                      Name = Role.Administrator,
                      Description = "Global Access",
                      Order = 30
                }, 
            new RoleType
                {
                    Name = Role.BarOwner,
                    Description = "Map Creator Access",
                    Order = 20
                }, 
            new RoleType
                {
                    Name = Role.User,
                    Description = "Only search access",
                    Order = 10
                }
        };

        #endregion

        #region User

        /// <summary>
        /// The user data.
        /// </summary>
        private static readonly IList<User> userData = new List<User>
        {
            new User
                {
                    Name = "nnn", 
                    Password = "nnn", 
                
                },
            new User
                {
                    Name = "user1", 
                    Password = "nnn", 
                
                },
            new User
                {
                    Name = "user2", 
                    Password = "nnn", 
                
                },
            new User
                {
                    Name = "admin", 
                    Password = "nnn", 
                
                },
            new User
                {
                    Name = "owner", 
                    Password = "nnn",                 
                }
        };

        #endregion User

        #region KitchenType

        /// <summary>
        /// The Kitchen Type
        /// </summary>
        private static readonly IList<KitchenType> kitchenTypeData = new List<KitchenType>
        {
            new KitchenType
                {
                   Name = "Bio-Restaurant",
                   Description = "Bio-Restaurant",
                   Order = 10
                }, 
            new KitchenType
                {
                   Name = "Bistro",
                   Description = "Bistro",
                   Order = 20
                },
            new KitchenType
                {
                   Name = "Burger",
                   Description = "Burger",
                   Order = 30
                },
            new KitchenType
                {
                   Name = "Dunkelrestaurant",
                   Description = "Dunkelrestaurant",
                   Order = 40
                }, 
            new KitchenType
                {
                   Name = "Erlebnisgastronomie",
                   Description = "Erlebnisgastronomie",
                   Order = 50
                },
            new KitchenType
                {
                   Name = "Fast Food",
                   Description = "Fast Food",
                   Order = 60
                },
            new KitchenType
                {
                   Name = "Feinkost-Delikatessen",
                   Description = "Feinkost-Delikatessen",
                   Order = 70
                },
            new KitchenType
                {
                   Name = "Fischrestaurant",
                   Description = "Fischrestaurant",
                   Order = 80
                },
            new KitchenType
                {
                   Name = "Gourmet",
                   Description = "Gourmet",
                   Order = 90
                },
            new KitchenType
                {
                   Name = "Imbiss",
                   Description = "Imbiss",
                   Order = 100
                },
            new KitchenType
                {
                   Name = "Institutionen",
                   Description = "Institutionen",
                   Order = 110
                },
            new KitchenType
                {
                   Name = "Kantinen",
                   Description = "Kantinen",
                   Order = 120
                },
            new KitchenType
                {
                   Name = "Kartoffelrestaurant",
                   Description = "Kartoffelrestaurant",
                   Order = 130
                },
            new KitchenType
                {
                   Name = "Koscher",
                   Description = "Koscher",
                   Order = 140
                },
            new KitchenType
                {
                   Name = "Michelin-Sterne",
                   Description = "Michelin-Sterne",
                   Order = 150
                },
            new KitchenType
                {
                   Name = "Oktoberfeste",
                   Description = "Oktoberfeste",
                   Order = 160
                },
            new KitchenType
                {
                   Name = "Pizzeria",
                   Description = "Pizzeria",
                   Order = 170
                },
            new KitchenType
                {
                   Name = "Steakhaus",
                   Description = "Steakhaus",
                   Order = 180
                },
            new KitchenType
                {
                   Name = "Prominenten-Lokale",
                   Description = "Prominenten-Lokale",
                   Order = 190
                },
            new KitchenType
                {
                   Name = "Street Food Markets",
                   Description = "Street Food Markets",
                   Order = 200
                },
            new KitchenType
                {
                   Name = "Suppenbars",
                   Description = "Suppenbars",
                   Order = 210
                },
            new KitchenType
                {
                   Name = "Sushi",
                   Description = "Sushi",
                   Order = 220
                },
            new KitchenType
                {
                   Name = "Vegan",
                   Description = "Vegan",
                   Order = 230
                },
            new KitchenType
                {
                   Name = "Vegetarisch",
                   Description = "Vegetarisch",
                   Order = 240
                }
        };

        #endregion KitchenType

        #region KitchenInternationalType

        /// <summary>
        /// ORIENTALISCHE
        /// </summary>
        public static Guid ORIENTALISCHE = Guid.NewGuid();

        /// <summary>
        /// SUDAMERIKA
        /// </summary>
        public static Guid SUDAMERIKA = Guid.NewGuid();

        /// <summary>
        /// ASIEN
        /// </summary>
        public static Guid ASIEN = Guid.NewGuid();

        /// <summary>
        /// NATIONALE
        /// </summary>
        public static Guid NATIONALE = Guid.NewGuid();

        /// <summary>
        /// The kitchen Type International
        /// </summary>
        private static readonly IList<KitchenInternationalType> kitchenTypeInternationalData = new List<KitchenInternationalType>
        {
            /////////////////ORIENTALISCHE///////////////////
            new KitchenInternationalType
                {
                   Id = ORIENTALISCHE,
                   ParentId = ORIENTALISCHE,
                   Name = "ORIENTALISCHE KÜCHE",
                   Description = "ORIENTALISCHE KÜCHE",
                   Order = 10
                },            
            new KitchenInternationalType
                {
                   ParentId = ORIENTALISCHE,
                   Name = "Orient",
                   Description = "Orient",
                   Order = 20
                },
            new KitchenInternationalType
                {
                   ParentId = ORIENTALISCHE,
                   Name = "Türkei",
                   Description = "Türkei",
                   Order = 30
                },
            new KitchenInternationalType
                {
                   ParentId = ORIENTALISCHE,
                   Name = "Marokko",
                   Description = "Marokko",
                   Order = 40
                }, 
            /////////////////SUDAMERIKA///////////////////
            new KitchenInternationalType
                {
                   Id = SUDAMERIKA,
                   ParentId = SUDAMERIKA,
                   Name = "NORD- UND SÜDAMERIKA",
                   Description = "NORD- UND SÜDAMERIKA",
                   Order = 10
                }, 
            new KitchenInternationalType
                {
                   ParentId = SUDAMERIKA,
                   Name = "USA",
                   Description = "USA",
                   Order = 20
                },
            new KitchenInternationalType
                {
                   ParentId = SUDAMERIKA,
                   Name = "Kanada",
                   Description = "Kanada",
                   Order = 30
                },
            new KitchenInternationalType
                {
                   ParentId = SUDAMERIKA,
                   Name = "Mexiko",
                   Description = "Mexiko",
                   Order = 40
                },
            new KitchenInternationalType
                {
                   ParentId = SUDAMERIKA,
                   Name = "Argentinien",
                   Description = "Argentinien",
                   Order = 50
                },  
            /////////////////ASIEN///////////////////
            new KitchenInternationalType
                {
                   Id = ASIEN,
                   ParentId = ASIEN,
                   Name = "ASIEN & PAZIFIK",
                   Description = "ASIEN & PAZIFIK",
                   Order = 10
                },            
            new KitchenInternationalType
                {
                   ParentId = ASIEN,
                   Name = "China",
                   Description = "China",
                   Order = 20
                },
            new KitchenInternationalType
                {
                   ParentId = ASIEN,
                   Name = "Japan",
                   Description = "Japan",
                   Order = 30
                },
            new KitchenInternationalType
                {
                   ParentId = ASIEN,
                   Name = "Thailand",
                   Description = "Thailand",
                   Order = 40
                },                 
            new KitchenInternationalType
                {
                   ParentId = ASIEN,
                   Name = "Indien",
                   Description = "Indien",
                   Order = 50
                }, 
            /////////////////NATIONALE///////////////////
            new KitchenInternationalType
                {
                   Id = NATIONALE,
                   ParentId = NATIONALE,
                   Name = "NATIONALE / REGIONALE KÜCHE",
                   Description = "NATIONALE / REGIONALE KÜCHE",
                   Order = 10
                },            
            new KitchenInternationalType
                {
                   ParentId = NATIONALE,
                   Name = "Bayern",
                   Description = "Bayern",
                   Order = 20
                },
            new KitchenInternationalType
                {
                   ParentId = NATIONALE,
                   Name = "Franken",
                   Description = "Franken",
                   Order = 30
                },
            new KitchenInternationalType
                {
                   ParentId = NATIONALE,
                   Name = "Südwestdeutschland",
                   Description = "Südwestdeutschland",
                   Order = 40
                },                 
            new KitchenInternationalType
                {
                   ParentId = NATIONALE,
                   Name = "Nordostdeutschland",
                   Description = "Nordostdeutschland",
                   Order = 50
                },                 
            new KitchenInternationalType
                {
                   ParentId = NATIONALE,
                   Name = "Sachsen, Thüringen",
                   Description = "Sachsen, Thüringen",
                   Order = 60
                }
        };

        #endregion

        #region KitchenMenuType

        /// <summary>
        /// The kitchenMenuTypeData
        /// </summary>
        private static readonly IList<KitchenMenuType> kitchenMenuTypeData = new List<KitchenMenuType>
        {
            new KitchenMenuType
            {
                Name = "Vorspeisen",
                Description = "Eine Vorspeise (franz. Entrée) ist eine kleine Speise, die vor dem Hauptgericht verzehrt wird und die den Appetit anregen.",
                Order = 1
            },         
            new KitchenMenuType
            {
                Name = "Hauptgerichte",
                Description = "Als Hauptgericht oder auch Hauptspeise (im Unterschied zu Vorspeisen und Nachspeisen) bezeichnet man das Gericht, das in einer Reihe von Gängen",
                Order = 2
            },  
            new KitchenMenuType
            {
                Name = "Spezialitäten",
                Description = "Eine Vorspeise (franz. Entrée) ist eine kleine Speise, die vor dem Hauptgericht verzehrt wird und die den Appetit anregen.",
                Order = 3
            },  
            new KitchenMenuType
            {
                Name = "Nachspeisen",
                Description = "Ein Dessert (auch Nachspeise, Nachtisch) ist eine meist süße („Süßspeise“), kalte oder warme Speise, die in der europäischen Küche üblicherweise bei einem",
                Order = 4
            },  
            new KitchenMenuType
            {
                Name = "Salate",
                Description = "in Salat als Speise ist eine Zubereitung, die ursprünglich und vorwiegend – jedoch nicht zwingend – aus rohen Salatpflanzen hergestellt wird.",
                Order = 5
            },  
        };

        #endregion

        #region SpecializationType
      
        public static Guid SpecializationType10 = Guid.NewGuid();
        public static Guid SpecializationType20 = Guid.NewGuid();
        public static Guid SpecializationType30 = Guid.NewGuid();
        public static Guid SpecializationType40 = Guid.NewGuid();
        public static Guid SpecializationType50 = Guid.NewGuid();
        public static Guid SpecializationType60 = Guid.NewGuid();
        public static Guid SpecializationType70 = Guid.NewGuid();

        /// <summary>
        /// The kitchen Type International
        /// </summary>
        private static readonly IList<SpecializationType> specializationTypeData = new List<SpecializationType>
        {            
            new SpecializationType
                {
                    Id = SpecializationType10,
                    ParentId = SpecializationType10,
                    Name = "ресторан",
                    Description = "ресторан",
                    Order = 10,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType20,
                    ParentId = SpecializationType20,
                    Name = "бар",
                    Description = "бар",
                    Order = 20,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType30,
                    ParentId = SpecializationType30,
                    Name = "клуб",
                    Description = "клуб",
                    Order = 30,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType40,
                    ParentId = SpecializationType40,
                    Name = "магазин",
                    Description = "магазин",
                    Order = 40,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType50,
                    ParentId = SpecializationType50,
                    Name = "выставка",
                    Description = "выставка",
                    Order = 50,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType60,
                    ParentId = SpecializationType60,
                    Name = "диско",
                    Description = "диско",
                    Order = 60,
                    Hierarchie = 1
                },
            new SpecializationType
                {
                    Id = SpecializationType70,
                    ParentId = SpecializationType70,
                    Name = "вечеринка",
                    Description = "вечеринка",
                    Order = 70,
                    Hierarchie = 1
                },
         
            #region 1. ресторан

            /// ресторан, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Bio-Restaurant",
                   Description = "Bio-Restaurant",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Bistro",
                   Description = "Bistro",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Burger",
                   Description = "Burger",
                   Order = 30,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Dunkelrestaurant",
                   Description = "Dunkelrestaurant",
                   Order = 40,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Erlebnisgastronomie",
                   Description = "Erlebnisgastronomie",
                   Order = 50,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Fast Food",
                   Description = "Fast Food",
                   Order = 60,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Feinkost-Delikatessen",
                   Description = "Feinkost-Delikatessen",
                   Order = 70,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Fischrestaurant",
                   Description = "Fischrestaurant",
                   Order = 80,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Gourmet",
                   Description = "Gourmet",
                   Order = 90,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Imbiss",
                   Description = "Imbiss",
                   Order = 100,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Institutionen",
                   Description = "Institutionen",
                   Order = 110,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Kantinen",
                   Description = "Kantinen",
                   Order = 120,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Kartoffelrestaurant",
                   Description = "Kartoffelrestaurant",
                   Order = 130,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Koscher",
                   Description = "Koscher",
                   Order = 140,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Michelin-Sterne",
                   Description = "Michelin-Sterne",
                   Order = 150,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Oktoberfeste",
                   Description = "Oktoberfeste",
                   Order = 160,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Pizzeria",
                   Description = "Pizzeria",
                   Order = 170,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Steakhaus",
                   Description = "Steakhaus",
                   Order = 180,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Prominenten-Lokale",
                   Description = "Prominenten-Lokale",
                   Order = 190,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Street Food Markets",
                   Description = "Street Food Markets",
                   Order = 200,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Suppenbars",
                   Description = "Suppenbars",
                   Order = 210,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Sushi",
                   Description = "Sushi",
                   Order = 220,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Vegan",
                   Description = "Vegan",
                   Order = 230,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Vegetarisch",
                   Description = "Vegetarisch",
                   Order = 240,
                   Hierarchie = 2
                },
            /// ресторан, Hierarchie = 3        
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Türkei",
                   Description = "Türkei",
                   Order = 10,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Italienische",
                   Description = "Italienische",
                   Order = 20,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Indische",
                   Description = "Indische",
                   Order = 30,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Mexiko",
                   Description = "Mexiko",
                   Order = 40,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "Argentinien",
                   Description = "Argentinien",
                   Order = 50,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType10,
                   Name = "China",
                   Description = "China",
                   Order = 60,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "Japan",
                   Description = "Japan",
                   Order = 70,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "ORIENTALISCHE KÜCHE",
                   Description = "ORIENTALISCHE KÜCHE",
                   Order = 80,
                   Hierarchie = 3
                },            
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "NORDAMERIKA",
                   Description = "NORDAMERIKA",
                   Order = 90,
                   Hierarchie = 3
                },            
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "SÜDAMERIKA",
                   Description = "SÜDAMERIKA",
                   Order = 100,
                   Hierarchie = 3
                },            
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "ASIEN & PAZIFIK",
                   Description = "ASIEN & PAZIFIK",
                   Order = 110,
                   Hierarchie = 3
                },            
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "INTERNATIONAL",
                   Description = "INTERNATIONAL",
                   Order = 120,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                  ParentId = SpecializationType10,
                   Name = "OST-EUROPEISCHE",
                   Description = "OST-EUROPEISCHE",
                   Order = 130,
                   Hierarchie = 3
                },            

            #endregion

            #region 2. бар

             /// бар, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "безалкогольный",
                   Description = "безалкогольный",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "винный",
                   Description = "винный",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "коктейль",
                   Description = "коктейль",
                   Order = 30,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "кофе/чай",
                   Description = "кофе/чай",
                   Order = 40,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "пиво /паб",
                   Description = "пиво /паб",
                   Order = 50,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "таверна",
                   Description = "таверна",
                   Order = 60,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "гриль",
                   Description = "гриль",
                   Order = 70,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "салат",
                   Description = "салат",
                   Order = 80,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "суши",
                   Description = "суши",
                   Order = 90,
                   Hierarchie = 2
                },
            /// бар, Hierarchie = 3
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Спорт",
                   Description = "Спорт",
                   Order = 10,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Диско",
                   Description = "Диско",
                   Order = 20,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Байкер",
                   Description = "Байкер",
                   Order = 30,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Гей",
                   Description = "Гей",
                   Order = 40,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Бикини",
                   Description = "Бикини",
                   Order = 50,
                   Hierarchie = 3
                },            
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Лобби",
                   Description = "Лобби",
                   Order = 60,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType20,
                   Name = "Экспресс",
                   Description = "Экспресс",
                   Order = 70,
                   Hierarchie = 3
                },

            #endregion бар

            #region 3. клуб

             /// клуб, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Sport",
                   Description = "Sport",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Fitness",
                   Description = "Fitness",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Gay",
                   Description = "Gay",
                   Order = 30,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Privat",
                   Description = "Privat",
                   Order = 40,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Sauna",
                   Description = "Sauna",
                   Order = 50,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Night",
                   Description = "Night",
                   Order = 60,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Kinder",
                   Description = "Kinder",
                   Order = 70,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType30,
                   Name = "Business",
                   Description = "Business",
                   Order = 80,
                   Hierarchie = 2
                },

            #endregion клуб

            #region 4. Магазин

             /// бар, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Lebensmittel",
                   Description = "Lebensmittel",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Одежда",
                   Description = "Одежда",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Обувь",
                   Description = "Обувь",
                   Order = 30,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Электротовары",
                   Description = "Электротовары",
                   Order = 40,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Мебель",
                   Description = "Мебель",
                   Order = 50,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Спорт",
                   Description = "Спорт",
                   Order = 60,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType40,
                   Name = "Gemischt",
                   Description = "Gemischt",
                   Order = 70,
                   Hierarchie = 2
                },

            #endregion клуб
            
            #region 5. Выставка

             /// Выставка, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType50,
                   Name = "Художественная",
                   Description = "Художественная",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType50,
                   Name = "Техническая",
                   Description = "Техническая",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType50,
                   Name = "Конференция",
                   Description = "Конференция",
                   Order = 30,
                   Hierarchie = 2
                },

            #endregion Выставка
            
            #region 6. Дискотека

             /// Дискотека, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Normal",
                   Description = "Normal",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Techno",
                   Description = "Techno",
                   Order = 20,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Ü30",
                   Description = "Ü30",
                   Order = 30,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Ü50",
                   Description = "Ü50",
                   Order = 40,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Карнавал",
                   Description = "Карнавал",
                   Order = 50,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Бар",
                   Description = "Бар",
                   Order = 60,
                   Hierarchie = 2
                },
            /// Дискотека, Hierarchie = 3
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "18+",
                   Description = "18+",
                   Order = 10,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Gay only",
                   Description = "Gay only",
                   Order = 20,
                   Hierarchie = 3
                },
            new SpecializationType
                {
                   ParentId = SpecializationType60,
                   Name = "Privat",
                   Description = "Privat",
                   Order = 30,
                   Hierarchie = 3
                },

            #endregion бар

            #region 7. Вечеринка

             /// Вечеринка, Hierarchie = 2
            new SpecializationType
                {
                   ParentId = SpecializationType70,
                   Name = "Global",
                   Description = "Global",
                   Order = 10,
                   Hierarchie = 2
                },
            new SpecializationType
                {
                   ParentId = SpecializationType70,
                   Name = "Privat",
                   Description = "Privat",
                   Order = 20,
                   Hierarchie = 2
                },

            #endregion Вечеринка

        };

        #endregion      

        #region Fields

        /// <summary>
        /// The _ctx.
        /// </summary>
        private readonly LpsContext _ctx;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FillDatabase"/> class.
        /// </summary>
        /// <param name="ctx">
        /// The ctx.
        /// </param>
        public FillDatabase(LpsContext ctx)
        {
            this._ctx = ctx;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The load.
        /// </summary>
        public void Load()
        {           
            if (this._ctx.Users.Count() > 0)
            {
                return;
            }

            try
            {
                foreach (User user in userData)
                {
                    this._ctx.Users.Add(user);
                }

                foreach (RoleType roleType in roleTypeData)
                {
                    this._ctx.RoleType.Add(roleType);
                }

                foreach (KitchenType kitchenType in kitchenTypeData)
                {
                    this._ctx.KitchenType.Add(kitchenType);
                }

                foreach (KitchenInternationalType kitchenInternationalType in kitchenTypeInternationalData)
                {
                    this._ctx.KitchenInternationalType.Add(kitchenInternationalType);
                }

                foreach (KitchenMenuType kitchenMenuType in kitchenMenuTypeData)
                {
                    this._ctx.KitchenMenuTypes.Add(kitchenMenuType);
                }

                foreach (SpecializationType specializationType in specializationTypeData)
                {
                    this._ctx.SpecializationTypes.Add(specializationType);
                }

                this._ctx.SaveChanges();

                UserRole role1 = new UserRole();
                role1.User = this._ctx.Users.FirstOrDefault(x => x.Name == "nnn");
                role1.RoleType = this._ctx.RoleType.FirstOrDefault(x => x.Name == Role.User);
                this._ctx.UserRole.Add(role1);

                UserRole role2 = new UserRole();
                role2.User = this._ctx.Users.FirstOrDefault(x => x.Name == "user1");
                role2.RoleType = this._ctx.RoleType.FirstOrDefault(x => x.Name == Role.User);
                this._ctx.UserRole.Add(role2);

                UserRole role3 = new UserRole();
                role3.User = this._ctx.Users.FirstOrDefault(x => x.Name == "user2");
                role3.RoleType = this._ctx.RoleType.FirstOrDefault(x => x.Name == Role.User);
                this._ctx.UserRole.Add(role3);

                UserRole role4 = new UserRole();
                role4.User = this._ctx.Users.FirstOrDefault(x => x.Name == "admin");
                role4.RoleType = this._ctx.RoleType.FirstOrDefault(x => x.Name == Role.Administrator);
                this._ctx.UserRole.Add(role4);

                UserRole role5 = new UserRole();
                role5.User = this._ctx.Users.FirstOrDefault(x => x.Name == "owner");
                role5.RoleType = this._ctx.RoleType.FirstOrDefault(x => x.Name == Role.BarOwner);
                this._ctx.UserRole.Add(role5);

                this._ctx.SaveChanges();

                var adminDevice = this._ctx.Devices.Create<Device>();
                adminDevice.DeviceId = "000";
                adminDevice.User = this._ctx.Users.FirstOrDefault(x => x.Name == "owner");
                this._ctx.Devices.Add(adminDevice);

                

                this._ctx.SaveChanges();


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", 
                        eve.Entry.Entity.GetType().Name, 
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            "- Property: \"{0}\", Error: \"{1}\"", 
                            ve.PropertyName, 
                            ve.ErrorMessage);
                    }
                }

                throw;
            }
            catch(Exception ex)
            {
                
            }
        }

        #endregion
    }
}