﻿namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;
    using global::Gameball.Constants;

    public class ValidateCouponResponse : GameballModel
    {
        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("startAt")]
        public DateTime? StartAt { get; set; }

        [JsonProperty("endsAt")]
        public DateTime? EndsAt { get; set; }

        [JsonProperty("entitledCollectionIds")]
        public IList<string> EntitledCollectionIds { get; set; }

        [JsonProperty("entitledProductIds")]
        public IList<string> EntitledProductIds { get; set; }

        [JsonProperty("oncePerCustomer")]
        public bool OncePerCustomer { get; set; }

        [JsonProperty("prerequisiteQuantityRange")]
        public double? PrerequisiteQuantityRange { get; set; }

        [JsonProperty("prerequisiteShippingPriceRange")]
        public double? PrerequisiteShippingPriceRange { get; set; }

        [JsonProperty("prerequisiteSubtotalRange")]
        public double? PrerequisiteSubtotalRange { get; set; }

        [JsonProperty("prerequisiteCollectionIds")]
        public IList<string> PrerequisiteCollectionIds { get; set; }

        [JsonProperty("prerequisiteProductIds")]
        public IList<string> PrerequisiteProductIds { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("usageLimit")]
        public int UsageLimit { get; set; }

        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("valueType")]
        public string ValueType { get; set; }

        [JsonProperty("cap")]
        public double? Cap { get; set; }
    }
}