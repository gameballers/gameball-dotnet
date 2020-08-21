namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class PlayerInfo : GameballModel
    {

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        [JsonProperty("referralLink")]
        public string ReferralLink { get; set; }

        [JsonProperty("scoreBalance")]
        public int ScoreBalance { get; set; }

        [JsonProperty("scoreName")]
        public string ScoreName { get; set; }

        [JsonProperty("level")]
        public Level Level { get; set; }

        [JsonProperty("balance")]
        public Balance Balance { get; set; }

        [JsonProperty("attributes")]
        public IList<Attribute> Attributes { get; set; }
    }
    public class Benefits:GameballModel
    {

        [JsonProperty("scoreEnteryReward")]
        public int ScoreEnteryReward { get; set; }

        [JsonProperty("pointsEnteryReward")]
        public int PointsEnteryReward { get; set; }

        [JsonProperty("levelDiscount")]
        public double? LevelDiscount { get; set; }

        [JsonProperty("discountCapping")]
        public double? DiscountCapping { get; set; }
    }

    public class Level:GameballModel
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("levelStartScore")]
        public int LevelStartScore { get; set; }

        [JsonProperty("levelOrder")]
        public int LevelOrder { get; set; }

        [JsonProperty("iconPath")]
        public string IconPath { get; set; }

        [JsonProperty("benefits")]
        public Benefits Benefits { get; set; }
    }

    public class Balance:GameballModel
    {

        [JsonProperty("pointsBalance")]
        public int PointsBalance { get; set; }

        [JsonProperty("pointsValue")]
        public double? PointsValue { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("pointsName")]
        public string PointsName { get; set; }
    }

    public class Attribute:GameballModel
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
