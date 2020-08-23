namespace Gameball.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using global::Gameball.Utils;
    using Newtonsoft.Json;
    public class InitializePlayerResponse : GameballModel
    {

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("displayName")]
            public string DisplayName { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("mobileNumber")]
            public string MobileNumber { get; set; }

            [JsonProperty("dateOfBirth")]
            public DateTime? DateOfBirth { get; set; }

            [JsonProperty("joinDate")]
            public DateTime? JoinDate { get; set; }

            [JsonProperty("clientId")]
            public int ClientId { get; set; }

            [JsonProperty("playerCategoryId")]
            public int PlayerCategoryId { get; set; }

            [JsonProperty("externalId")]
            public string ExternalId { get; set; }

            [JsonProperty("referralCode")]
            public string ReferralCode { get; set; }

            [JsonProperty("referredById")]
            public object ReferredById { get; set; }

            [JsonProperty("currentLevel")]
            public int CurrentLevel { get; set; }

            [JsonProperty("level")]
            public object Level { get; set; }

            [JsonProperty("accFrubies")]
            public int AccFrubies { get; set; }

            [JsonProperty("accPoints")]
            public int AccPoints { get; set; }

            [JsonProperty("isActive")]
            public bool IsActive { get; set; }

            [JsonProperty("isMigrate")]
            public bool IsMigrate { get; set; }

            [JsonProperty("referralActivationDate")]
            public object ReferralActivationDate { get; set; }

            [JsonProperty("dynamicLink")]
            public object DynamicLink { get; set; }

            [JsonProperty("dynamicPreviewLink")]
            public object DynamicPreviewLink { get; set; }

            [JsonProperty("transactions")]
            public IList<object> Transactions { get; set; }

            [JsonProperty("holdTransaction")]
            public IList<object> HoldTransaction { get; set; }

            [JsonProperty("otPs")]
            public IList<object> OtPs { get; set; }

            [JsonProperty("achievements")]
            public IList<object> Achievements { get; set; }

            [JsonProperty("isDeleted")]
            public bool IsDeleted { get; set; }

            [JsonProperty("creationDate")]
            public DateTime? CreationDate { get; set; }

            [JsonProperty("lastUpdate")]
            public DateTime? LastUpdate { get; set; }
    }
}
