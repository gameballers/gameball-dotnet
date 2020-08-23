namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class PlayerInfoRequest : GameballModel
    {

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

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
            return true;
        }

    }
  

}
