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
        public string displayName { get; set; }

        [JsonProperty("referralCode")]
        public string referralCode { get; set; }

        [JsonProperty("referralLink")]
        public string referralLink { get; set; }

        [JsonProperty("score")]
        public Score score { get; set; }

        [JsonProperty("level")]
        public Level level { get; set; }

        [JsonProperty("points")]
        public Points points { get; set; }

        [JsonProperty("playerAttributes")]
        public IList<PlayerAttribute> playerAttributes { get; set; }
    }
    public class Score
    {

        [JsonProperty("scoreBalance")]
        public int scoreBalance { get; set; }

        [JsonProperty("scoreName")]
        public string scoreName { get; set; }
    }

    public class Benefits
    {

        [JsonProperty("scoreEnteryReward")]
        public int scoreEnteryReward { get; set; }

        [JsonProperty("pointsEnteryReward")]
        public int pointsEnteryReward { get; set; }

        [JsonProperty("levelDiscount")]
        public object levelDiscount { get; set; }

        [JsonProperty("discountCapping")]
        public object discountCapping { get; set; }
    }

    public class Level
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("description")]
        public object description { get; set; }

        [JsonProperty("levelStartScore")]
        public int levelStartScore { get; set; }

        [JsonProperty("levelOrder")]
        public int levelOrder { get; set; }

        [JsonProperty("iconPath")]
        public string iconPath { get; set; }

        [JsonProperty("benefits")]
        public Benefits benefits { get; set; }
    }

    public class Points
    {

        [JsonProperty("pointsBalance")]
        public int pointsBalance { get; set; }

        [JsonProperty("pointsValue")]
        public int pointsValue { get; set; }

        [JsonProperty("currency")]
        public string currency { get; set; }

        [JsonProperty("pointsName")]
        public string pointsName { get; set; }
    }

    public class PlayerAttribute
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

}
