namespace Gameball.Models
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using global::Gameball.Utils;

    /// <summary>
    /// A Gameball model base class, parent class for all Gameball models.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class GameballModel
    {
        /// <summary>Serializes the Gameball model as a JSON string.</summary>
        /// <returns>An indented JSON string represensation of the object.</returns>
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this,Formatting.Indented);
        }

        /// <summary>
        /// Validates the Model before sending the request.
        /// </summary>
        internal virtual bool Validate()
        {
            return true;
        }
    }
}

 
