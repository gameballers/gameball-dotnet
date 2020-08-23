namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;

    public class CreateCouponRequest : GameballModel
    {
        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty ("startAt")]
        public DateTime? StartAt { get; set; }

        [JsonProperty("endsAt")]
        public DateTime? EndsAt { get; set; }

        [JsonProperty ("entitledCollectionIds")]
        public IList<string> EntitledCollectionIds { get; set; }

        [JsonProperty("entitledProductIds")]
        public IList<string> EntitledProductIds { get; set; }

        [JsonProperty("oncePerCustomer")]
        public bool OncePerCustomer { get; set; }

        [JsonProperty ("prerequisiteQuantityRange")]
        public double PrerequisiteQuantityRange { get; set; }

        [JsonProperty("prerequisiteShippingPriceRange")]
        public double PrerequisiteShippingPriceRange { get; set; }

        [JsonProperty("prerequisiteSubtotalRange")]
        public double PrerequisiteSubtotalRange { get; set; }

        [JsonProperty("prerequisiteCollectionIds")]
        public IList<string> PrerequisiteCollectionIds { get; set; }

        [JsonProperty("prerequisiteProductIds")]
        public IList<string> PrerequisiteProductIds { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("usageLimit")]
        public int UsageLimit{ get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("valueType")]
        public int ValueType { get; set; }

        [JsonProperty("cap")]
        public double Cap { get; set; }

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

            if (StartAt != null && EndsAt !=null && DateTime.Compare((DateTime)StartAt,(DateTime)EndsAt)>0)
            {
                throw new ArgumentException("A Coupon cannot have a start date after its end date.", nameof(StartAt));
            }

            return true;
        }
    }
}