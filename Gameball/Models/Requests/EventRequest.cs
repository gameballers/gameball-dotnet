                                                                                                                                                                                                                                                                                                                                                                                                                                                             
namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;
    using System.ComponentModel;

    public class EventRequest:GameballModel
    {

        [JsonProperty("events")]
        internal Dictionary<string, Dictionary<string, object>> Events { get; set; }

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("playerAttributes")]
        public PlayerAttributes PlayerAttributes { get; set; }

        [JsonProperty("isPositive")]
        public bool IsPositive { get; set; }

        [JsonIgnore]
        public Int32 PlayerTypeId { get; set; }

        [JsonProperty("sessionInfo")]
        public SessionInfo SessionInfo { get; set; }

        [JsonProperty("isMessageTrigger")]
        public bool IsMessageTrigger { get; set; }
        internal override bool Validate()
        {
            if (PlayerUniqueId == null || PlayerUniqueId.Length == 0)
            {
                throw new ArgumentException("Player Unique ID cannot be null or an empty string.", nameof(PlayerUniqueId));
            }

            if (PlayerUniqueId != null && GameballUtils.ContainsWhitespace(PlayerUniqueId))
            {
                throw new ArgumentException("Player Unique ID cannot contain whitespace.", nameof(PlayerUniqueId));
            }

            if (PlayerAttributes != null)
            {
                PlayerAttributes.Validate();
            }

            if (SessionInfo!=null)
            {
                SessionInfo.Validate();
            }

            return true;
        }
        /// <summary>
        /// Adds an <see cref="Event">event</see> to the Event request before sending it.
        /// </summary>
        /// <remarks>
        /// You should always prefer using this method in adding events to the request over
        /// the traditional one of sending events as dictionaries.
        /// </remarks>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool AddEvent(Event Event)
        {
            if (Events == null)
                this.Events = new Dictionary<string, Dictionary<string, object>>();
            if (Event.Name == null || Event.Name.Length == 0)
            {
                throw new ArgumentException("Event name can not be null or empty string.", nameof(EventRequest));
            }
            else if (Events.ContainsKey(Event.Name))
            {
                throw new ArgumentException("Event already exists.", nameof(EventRequest));
            }
            else
            {
                Events.Add(Event.Name,Event.Metadata);
                return true;
            }
        }

        /// <summary>
        /// Removes an <see cref="Event">event</see> from the Event request before sending it.
        /// </summary>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool RemoveEvent(Event Event)
        {
            if (Events == null)
                this.Events = new Dictionary<string, Dictionary<string, object>>();
            if (Event.Name == null || Event.Name.Length == 0)
            {
                throw new ArgumentException("Event name can not be null or empty string.", nameof(EventRequest));
            }
            else if (!Events.ContainsKey(Event.Name))
            {
                throw new ArgumentException("Event does not exist.", nameof(EventRequest));
            }
            else
            {
                Events.Remove(Event.Name);
                return true;
            }
        }


    }

    public class Geolocation:GameballModel
    {

        [JsonProperty("latitude")]
        public int Latitude { get; set; }

        [JsonProperty("longitude")]
        public int Longitude { get; set; }

    }

    public class SessionInfo:GameballModel
    {

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("referer")]
        public string Referer { get; set; }

        [JsonProperty("platform")]
        public int Platform { get; set; }

        [JsonProperty("geolocation")]
        public Geolocation Geolocation { get; set; }

        internal override bool Validate()
        {
            if (Geolocation != null)
            {
                Geolocation.Validate();
            }
            return true;
        }

    }

    public class Event
    {
    public string Name { get; set; }

     internal Dictionary<string,object> Metadata { get; set; }

        /// <summary>
        /// Adds a <see cref="Metadata">metadata</see> to the <see cref="Event">event </see> object before sending it.
        /// </summary>
        /// <remarks>
        /// You should always prefer using this method in adding metadata to the event over
        /// the traditional one of sending metadata as a dictionary.
        /// </remarks>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool AddMetadata(string name,object value)
        {
            if (Metadata == null)
                this.Metadata = new Dictionary<string, object>();
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("Metadata name can not be null or empty string.", nameof(Event));
            }
            else if (Metadata.ContainsKey(name))
            {
                throw new ArgumentException("Metadata already exists.", nameof(Event));
            }
            else
            {
                Metadata.Add(name, value);
                return true;
            }
        }

        /// <summary>
        /// Removes a <see cref="metadata">metadata</see> from the <see cref="Event">event </see> object before sending it.
        /// </summary>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool RemoveMetadata(string name)
        {
            if (Metadata == null)
                this.Metadata = new Dictionary<string, object>();
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("Metadata name can not be null or empty string.", nameof(Event));
            }
            else if (!Metadata.ContainsKey(name))
            {
                throw new ArgumentException("Metadata does not exist.", nameof(Event));
            }
            else
            {
                Metadata.Remove(name);
                return true;
            }

        }


    } 


}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      