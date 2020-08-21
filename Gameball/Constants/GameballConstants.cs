namespace Gameball.Constants
{

    /// <summary>
    /// Gameball Constants, includes available endpoints,base URL along with different languages supported and their codes.
    /// </summary>
    internal static class GameballConstants
    {
        //The base URL for Gameball v2.0 API
        public const string BaseURL = "https://gb-api.azurewebsites.net/api/v2.0/";

        //Available endpoints
        public const string Player = "Integrations/Player";
        public const string Event = "Integrations/Event";
        public const string Referral = "Integrations/Referral";

        public const string Balance = "Integrations/Transaction/Balance";
        public const string Hold = "Integrations/Transaction/Hold";
        public const string Redeem = "Integrations/Transaction/Redeem";
        public const string Cancel = "Integrations/Transaction/Cancel";
        public const string Reward = "Integrations/Transaction/Reward";
        public const string Action = "Integrations/Action";
        public const string Coupon = "Integrations/Coupon";
        public const string RedeemDiscount = "Integrations/Coupon/Redeem";
        public const string ValidateDiscount = "Integrations/Coupon/Validate";
        public const string PlayerInfo = "Integrations/Player/Info";

        //Supported languages
        public const string English = "en";
        public const string Arabic = "ar";
        public const string French = "fr";
    }
}

