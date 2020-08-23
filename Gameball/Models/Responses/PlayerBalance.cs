namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class PlayerBalance : GameballModel
    {


            [JsonProperty("pointsBalance")]
            public int PointsBalance { get; set; }

            [JsonProperty("pointsValue")]
            public double? PointsValue { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("walletPointsName")]
            public string WalletPointsName { get; set; }

            [JsonProperty("isDeleted")]
            public bool IsDeleted { get; set; }

            [JsonProperty("creationDate")]
            public object CreationDate { get; set; }

            [JsonProperty("lastUpdate")]
            public object LastUpdate { get; set; }
    }
}
