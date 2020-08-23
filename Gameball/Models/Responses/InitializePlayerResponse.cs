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

            [JsonProperty("gameballId")]
            public long GameballId { get; set; }

            
    }
}
