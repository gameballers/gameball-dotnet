namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class ActionResponse : GameballModel
    {

        [JsonProperty("redeemResponse")]
        public TransactionResponse RedeemResponse { get; set; }

        [JsonProperty("rewardResponse")]
        public TransactionResponse RewardResponse { get; set; }

        [JsonProperty("eventResponse")]
        public object EventResponse { get; set; }

    }
}
