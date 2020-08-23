namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;
    using global::Gameball.Utils;

    public class ReferralRequest:GameballModel
    {

        [JsonProperty("playerCode")]
        public string PlayerCode { get; set; }

        [JsonProperty("playerUniqueId")]
        public string PlayerUniqueId { get; set; }

        [JsonProperty("playerAttributes")]
        public PlayerAttributes PlayerAttributes { get; set; }

        [JsonProperty("sessionInfo")]
        public SessionInfo SessionInfo { get; set; }

        [JsonProperty("isMessageTrigger")]
        public bool IsMessageTrigger { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("creationDate")]
        internal DateTime CreationDate { get; set; }

        [JsonProperty("lastUpdate")]
        internal DateTime LastUpdate { get; set; }

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

            if (PlayerCode == null || PlayerCode.Length == 0)
            {
                throw new ArgumentException("Player Code cannot be null or an empty string.", nameof(PlayerCode));
            }

            if (PlayerCode != null && GameballUtils.ContainsWhitespace(PlayerCode))
            {
                throw new ArgumentException("Player Code cannot contain whitespace.", nameof(PlayerCode));
            }

            if (PlayerAttributes != null)
            {
                PlayerAttributes.Validate();
            }

            if (SessionInfo != null)
            {
                SessionInfo.Validate();
            }

            return true;



        }

    }
}
