
namespace Gameball.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// A Gameball custom exception class.
    /// </summary>
    public class GameballException:Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        //Gameball internal error code
        [JsonProperty("code")]
        public uint GameballCode { get; set; }

        //Gameball internal error message
        [JsonProperty("message")]
        public string GameballMessage { get; set; }

        public GameballException(HttpStatusCode httpStatusCode,string gameballMessage=null, uint gameballCode=0) :base(gameballMessage)
        {
            this.HttpStatusCode = httpStatusCode;
            this.GameballCode = gameballCode;
            this.GameballMessage = gameballMessage;
        }


    }
}
