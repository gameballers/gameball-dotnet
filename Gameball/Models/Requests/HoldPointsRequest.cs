﻿namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;

    public class HoldPointsRequest : GameballModel
    {
        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("amount")]
        public Double Amount { get; set; }

        [JsonProperty("holdReference")]
        public string HoldReference { get; set; }

        [JsonProperty("otp")]
        public string Otp { get; set; }

        [JsonProperty("transactionTime")]
        internal DateTime TransactionTime { get; set; }

        [JsonProperty("hash")]
        internal string Hash { get; set; }

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

            if (this.HoldReference != null && this.HoldReference.Length == 0)
            {
                throw new ArgumentException("Hold Reference cannot be the empty string.", nameof(HoldReference));
            }

            if (this.HoldReference != null && GameballUtils.ContainsWhitespace(HoldReference))
            {
                throw new ArgumentException("Hold Reference cannot contain whitespace.", nameof(HoldReference));
            }


            if (Amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative", nameof(Amount));
            }

            return true;



        }
    }
}