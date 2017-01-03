// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFormatterConfigAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   The xml formatter config attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer
{
    using System;
    using System.Net.Http.Formatting;
    using System.Web.Http.Controllers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The xml formatter config attribute.
    /// </summary>
    public class XmlFormatterConfigAttribute : Attribute, IControllerConfiguration
    {
        #region Public Methods and Operators

        /// <summary>
        /// The initialize.
        /// </summary>
        /// <param name="controllerSettings">
        /// The controller settings.
        /// </param>
        /// <param name="controllerDescriptor">
        /// The controller descriptor.
        /// </param>
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Clear();
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore,
                    TypeNameHandling =
                        TypeNameHandling.None,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };
            controllerSettings.Formatters.Add(formatter);
            controllerSettings.Formatters.Add(new XmlMediaTypeFormatter());
        }

        #endregion
    }
}