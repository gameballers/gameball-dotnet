namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class TransactionResponse : GameballModel
    {

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("amount")]
        public Double? Amount { get; set; }

        [JsonProperty("gameballTransactionId")]
        public Int64 GameballTransactionId { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("holdReference")]
        public string HoldReference { get; set; }

        [JsonProperty("otp")]
        public object Otp { get; set; }

        [JsonProperty("transactionTime")]
        public DateTime TransactionTime { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
