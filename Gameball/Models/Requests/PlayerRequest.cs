namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class PlayerRequest : GameballModel
    {

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonIgnore]
        public Int32 PlayerTypeId { get; set; }

        [JsonProperty("playerAttributes")]
        public PlayerAttributes PlayerAttributes { get; set; }

        [JsonProperty("deviceToken")]
        public string DeviceToken { get; set; }

        [JsonProperty("osType")]
        public string OsType { get; set; }

        [JsonProperty("isDeleted")]
        internal bool IsDeleted { get; set; }

        [JsonProperty("creationDate")]
        internal DateTime CreationDate { get; set; }

        [JsonProperty("lastUpdate")]
        internal DateTime LastUpdate { get; set; }

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

            return true;
        }


    }
    public class PlayerAttributes : GameballModel
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("joinDate")]
        public DateTime JoinDate { get; set; }

        [JsonProperty("custom")]
        internal Dictionary<string, object> Custom { get; set; }

        internal override bool Validate()
        {
            if (Email != null && !GameballUtils.IsValidEmail(Email))
            {
                throw new ArgumentException("This is not a valid Email address.", nameof(Email));
            }

            if (DateOfBirth != DateTime.MinValue && DateTime.Compare(DateOfBirth,DateTime.Now)>0)
            {
                throw new ArgumentException("Birthdate cannot be a future date.", nameof(DateOfBirth));
            }

            if (JoinDate != DateTime.MinValue && DateTime.Compare(JoinDate, DateTime.Now) > 0)
            {
                throw new ArgumentException("Joindate cannot be a future date.", nameof(JoinDate));
            }
            return true;
        }

        /// <summary>
        /// Adds a <see cref="CustomPlayerAttribute">custom attribute</see> to the <see cref="PlayerAttributes">player </see> object before passing it to the service.
        /// </summary>
        /// <remarks>
        /// You should always prefer using this method in adding custom player attributes to the player over
        /// the traditional one of sending custom attributes as a dictionary.
        /// </remarks>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool AddCustomAttribute(string name , object value)
        {
            if (Custom == null)
                this.Custom = new Dictionary<string, object>();
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("Custom attribute name can not be null or empty string.", nameof(PlayerAttributes));
            }
            else if (Custom.ContainsKey(name))
            {
                throw new ArgumentException("Custom attribute already exists.", nameof(PlayerAttributes));
            }
            else
            {
                Custom.Add(name, value);
                return true;
            }
        }

        /// <summary>
        /// Removes a <see cref="CustomPlayerAttribute">custom attribute</see> from the <see cref="PlayerAttributes">player </see> object before passing it to the service.
        /// </summary>
        /// <returns> a <see cref="bool">boolean </see>that represents the success or failure of the process.</returns>
        public bool RemoveCustomAttribute(string name)
        {
            if (Custom == null)
                this.Custom = new Dictionary<string, object>();
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("Custom attribute name can not be null or empty string.", nameof(PlayerAttributes));
            }
            else if (!Custom.ContainsKey(name))
            {
                throw new ArgumentException("Custom attribute does not exist.", nameof(PlayerAttributes));
            }
            else
            {
                Custom.Remove(name);
                return true;
            }
        }

    }



    }
