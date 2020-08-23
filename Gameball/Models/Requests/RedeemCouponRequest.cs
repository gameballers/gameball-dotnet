namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;

    public class RedeemCouponRequest : GameballModel
    {
        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

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

            if (Code == null)
            {
                throw new ArgumentException("Coupon code cannot be null.", nameof(Code));
            }

            return true;
        }
    }
}