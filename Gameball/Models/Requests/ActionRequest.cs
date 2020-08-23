                                                                                                                                                                                                                                                                                                          
namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;
    using System.ComponentModel;

    public class ActionRequest:GameballModel
    {

        [JsonProperty("events")]
        internal Dictionary<string, Dictionary<string, object>> Events { get; set; }

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("playerAttributes")]
        public PlayerAttributes PlayerAttributes { get; set; }

        [JsonProperty("pointsTransaction")]
        public PointsTransaction PointsTransaction { get; set; }

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

            if (PointsTransaction!=null)
            {
                PointsTransaction.Validate();
            }


            if (PlayerAttributes != null)
            {
                PlayerAttributes.Validate();
            }

            return true;
        }
        /// <summary>
        /// Adds an <see cref="Event">event</see> to the Action request before sending it.
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
        /// Removes an <see cref="Event">event</see> from the Action request before sending it.
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

    public class PointsTransaction:GameballModel
    {

        [JsonProperty("rewardAmount")]
        public Double RewardAmount { get; set; }

        [JsonProperty("holdReference")]
        public string HoldReference { get; set; }

        [JsonProperty("transactionTime")]
        internal DateTime TransactionTime { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("hash")]
        internal string Hash { get; set; }

        internal override bool Validate()
        {
            if (this.HoldReference != null && this.HoldReference.Length == 0)
            {
                throw new ArgumentException("Hold Reference cannot be the empty string.", nameof(HoldReference));
            }

            if (this.HoldReference != null && GameballUtils.ContainsWhitespace(HoldReference))
            {
                throw new ArgumentException("Hold Reference cannot contain whitespace.", nameof(HoldReference));
            }

            try
            {
                int Amount = Convert.ToInt32(RewardAmount);
            }

            catch
            {
                throw new ArgumentException("Reward Amount must be a valid Amount ", nameof(RewardAmount));
            }

            if (Convert.ToInt32(RewardAmount) < 0 )
            {
                throw new ArgumentException("Reward Amount cannot be negative", nameof(RewardAmount));
            }

            return true;



        }
    }

    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     