// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRContractResolver.cs" company="">
//   
// </copyright>
// <summary>
//   The signal r contract resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer
{
    using System;
    using System.Reflection;

    using Microsoft.AspNet.SignalR.Infrastructure;

    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The signal r contract resolver.
    /// </summary>
    public class SignalRContractResolver : IContractResolver
    {
        #region Fields

        /// <summary>
        /// The assembly.
        /// </summary>
        private readonly Assembly assembly;

        /// <summary>
        /// The camel case contract resolver.
        /// </summary>
        private readonly IContractResolver camelCaseContractResolver;

        /// <summary>
        /// The default contract serializer.
        /// </summary>
        private readonly IContractResolver defaultContractSerializer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRContractResolver"/> class.
        /// </summary>
        public SignalRContractResolver()
        {
            this.defaultContractSerializer = new DefaultContractResolver();
            this.camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            this.assembly = typeof(Connection).Assembly;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The resolve contract.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="JsonContract"/>.
        /// </returns>
        public JsonContract ResolveContract(Type type)
        {
            if (type.Assembly.Equals(this.assembly))
            {
                return this.defaultContractSerializer.ResolveContract(type);
            }

            return this.camelCaseContractResolver.ResolveContract(type);
        }

        #endregion
    }
}