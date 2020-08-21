namespace Gameball
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using global::Gameball.Constants;
    using System.Text;
    using global::Gameball.Models;
    using System.ComponentModel;
    using global::Gameball.Exceptions;
    using global::Gameball.Utils;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// A Gameball client, used for the communication with Gameball's API through issuing requests and deserializing responses.
    /// </summary>
    public class Gameball
    {
        /// <summary>
        /// The base URL for Gameball's API. Defaults to <see cref="GameballConstants.BaseURL">BaseURL</see>.
        /// </summary>
        string ApiBase { get; }
        /// <summary>
        /// The API key used by the client to make requests.
        /// </summary>
        /// <remarks>
        /// You can find it in your <see href="https://help.gameball.co/en/articles/3467114-get-your-account-integration-details-api-key-and-transaction-key">Gameball Dashboard </see>
        /// </remarks>
        string ApiKey { get; }
        /// <summary>
        /// The Transaction key used by the client to make requests.
        /// </summary>
        /// <remarks>
        /// It is mandatory for services that include transactions.
        /// You can find it in your <see href="https://help.gameball.co/en/articles/3467114-get-your-account-integration-details-api-key-and-transaction-key">Gameball Dashboard </see>
        /// </remarks>
        string TransactionKey { get; }
        /// <summary>
        /// Your preferred Gameball Language.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="GameballConstants.English">English.</see>
        /// </remarks>
        string Lang { get; set; }

        /// <summary>
        /// Gameball client responsible for handling http requests.
        /// </summary>
        HttpClient Client { get; }

        /// <summary>Initializes a new instance of the <see cref="Gameball"/> class.</summary>
        /// <param name="apiKey">The API key used by the client to make requests.</param>
        /// <param name="transactionKey">The Transaction key used by the client to make requests.</param>
        /// <exception cref="ArgumentNullException">if <c>apiKey</c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// if <c>apiKey</c> is empty or contains whitespace.
        /// if <c>transactionKey</c> is empty or contains whitespace.
        /// </exception>
        public Gameball(string apiKey, string transactionKey = null)
        {
            Client = new HttpClient();
            if (apiKey == null || apiKey.Length == 0)
            {
                throw new ArgumentException("API key cannot be null or an empty string.", nameof(apiKey));
            }

            if (apiKey != null && GameballUtils.ContainsWhitespace(apiKey))
            {
                throw new ArgumentException("API key cannot contain whitespace.", nameof(apiKey));
            }

            if (transactionKey != null && GameballUtils.ContainsWhitespace(transactionKey))
            {
                throw new ArgumentException("Transaction key cannot contain whitespace.", nameof(transactionKey));
            }

            if (transactionKey != null && transactionKey.Length == 0)
            {
                throw new ArgumentException("Transaction key cannot be the empty string.", nameof(transactionKey));
            }



            this.ApiBase = GameballConstants.BaseURL;
            this.ApiKey = apiKey;
            this.TransactionKey = transactionKey;
            this.Lang = GameballConstants.English;
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("APIKey", ApiKey);
            Client.DefaultRequestHeaders.Add("lang", Lang);
        }


        private static GameballException BuildGameballException(HttpResponseMessage response)
        {
            JObject jObject = null;

            //If response's content is not Json object (it throws Json exception while parsing),
            //then the exception is related to authorization 401.
            //otherwise throw the Gameball exception received in Json.
            try
            {
                jObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Newtonsoft.Json.JsonException)
            {
                return new GameballException(response.StatusCode, "ApiKey invalid");
            }

            return BuildInvalidResponseException(response);
        }

        /// <summary>
        /// Deserializes the Json response to Gameball exception.
        /// </summary>
        private static GameballException BuildInvalidResponseException(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<GameballException>(response.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// This service For Create Player or Register Player's Device.
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public InitializePlayerResponse InitializePlayer(PlayerRequest player)
        {
            player.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Player, new StringContent(player.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<InitializePlayerResponse>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// This Async service For Create Player or Register Player's Device.
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<InitializePlayerResponse> InitializePlayerAsync(PlayerRequest player)
        {
            player.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Player, new StringContent(player.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<InitializePlayerResponse>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// This service is used by Clients to send Actions to Gameball 
        /// based on Events triggered by the Players Actions on the Client's Interface.
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public bool SendEvent(EventRequest events)
        {
            events.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Event, new StringContent(events.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;

        }

        /// <summary>
        /// This Async service is used by Clients to send Actions to Gameball 
        /// based on Events triggered by the Players Actions on the Client's Interface.
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<bool> SendEventAsync(EventRequest events)
        {
            events.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Event, new StringContent(events.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// AddReferral Bussiness. it add new player and add actions for all referral challenges. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public bool CreateReferral(ReferralRequest referral)
        {
            referral.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Referral, new StringContent(referral.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;

        }
        /// <summary>
        /// Async AddReferral Bussiness. it add new player and add actions for all referral challenges. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<bool> CreateReferralAsync(ReferralRequest referral)
        {
            referral.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Referral, new StringContent(referral.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;

        }
        /// <summary>
        /// This service For Get Player Balances. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public PlayerBalance GetPlayerBalance(string playerUniqueId)
        {
            PlayerBalanceRequest Balance = new PlayerBalanceRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey)
            };
            Balance.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Balance, new StringContent(Balance.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<PlayerBalance>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Get Player Balances. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<PlayerBalance> GetPlayerBalanceAsync(string playerUniqueId)
        {
            PlayerBalanceRequest Balance = new PlayerBalanceRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey)
            };
            Balance.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Balance, new StringContent(Balance.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<PlayerBalance>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Holding or Unholding Transaction. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public TransactionResponse HoldPoints(HoldPointsRequest hold)
        {
            hold.Validate();
            hold.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(hold.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(hold.TransactionTime), GameballUtils.ToValidAmount(hold.Amount));
            hold.Hash = hash;
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Hold, new StringContent(hold.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Holding or Unholding Transaction. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<TransactionResponse> HoldPointsAsync(HoldPointsRequest hold)
        {
            hold.Validate();
            hold.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(hold.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(hold.TransactionTime), GameballUtils.ToValidAmount(hold.Amount));
            hold.Hash = hash;

            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Hold, new StringContent(hold.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Redeem Points Based on the Amount send and the Client Transaction Config. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public TransactionResponse RedeemPoints(RedeemPointsRequest redeem)
        {
            redeem.Validate();
            redeem.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(redeem.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(redeem.TransactionTime));
            redeem.Hash = hash;
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Redeem, new StringContent(redeem.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(response.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// This Async service For Redeem Points Based on the Amount send and the Client Transaction Config. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<TransactionResponse> RedeemPointsAsync(RedeemPointsRequest redeem)
        {
            redeem.Validate();
            redeem.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(redeem.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(redeem.TransactionTime));
            redeem.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Redeem, new StringContent(redeem.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Reversing a Transaction. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public TransactionResponse ReverseTransaction(ReverseTransactionRequest reverse)
        {
            reverse.Validate();
            reverse.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(reverse.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(reverse.TransactionTime), GameballUtils.ToValidAmount(reverse.Amount));
            reverse.Hash = hash;
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Cancel, new StringContent(reverse.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Reversing a Transaction. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<TransactionResponse> ReverseTransactionAsync(ReverseTransactionRequest reverse)
        {
            reverse.Validate();
            reverse.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(reverse.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(reverse.TransactionTime), GameballUtils.ToValidAmount(reverse.Amount));
            reverse.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Cancel, new StringContent(reverse.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Reversing a Hold process. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public TransactionResponse ReverseHold(ReverseHoldRequest hold)
        {
            hold.Validate();
            hold.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(hold.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(hold.TransactionTime), GameballUtils.ToValidAmount(hold.Amount));
            hold.Hash = hash;

            var response = this.Client.PostAsync(ApiBase + GameballConstants.Hold, new StringContent(hold.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Reversing a Hold process. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<TransactionResponse> ReverseHoldAsync(ReverseHoldRequest hold)
        {
            hold.Validate();
            hold.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(hold.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(hold.TransactionTime), GameballUtils.ToValidAmount(hold.Amount));
            hold.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Hold, new StringContent(hold.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Reward Points Based on the Amount send and the Client Transaction Config. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public TransactionResponse RewardPoints(RewardPointsRequest reward)
        {
            reward.Validate();
            reward.TransactionTime = DateTime.UtcNow;
            string Hash = GameballUtils.GetSHA1(reward.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(reward.TransactionTime), GameballUtils.ToValidAmount(reward.Amount));
            reward.Hash = Hash;

            var response = this.Client.PostAsync(ApiBase + GameballConstants.Reward, new StringContent(reward.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Reward Points Based on the Amount send and the Client Transaction Config. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<TransactionResponse> RewardPointsAsync(RewardPointsRequest reward)
        {
            reward.Validate();
            reward.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(reward.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(reward.TransactionTime), GameballUtils.ToValidAmount(reward.Amount));
            reward.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Reward, new StringContent(reward.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<TransactionResponse>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service For Get the Player's Info. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public PlayerInfo GetPlayerInfo(string playerUniqueId)
        {
            PlayerInfoRequest Info = new PlayerInfoRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey)
            };
            Info.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.PlayerInfo, new StringContent(Info.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<PlayerInfo>(response.Content.ReadAsStringAsync().Result);


        }
        /// <summary>
        /// This Async service For Get the Player's Info. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<PlayerInfo> GetPlayerInfoAsync(string playerUniqueId)
        {
            PlayerInfoRequest Info = new PlayerInfoRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey)
            };
            Info.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.PlayerInfo, new StringContent(Info.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<PlayerInfo>(await response.Content.ReadAsStringAsync());


        }
        /// <summary>
        /// This service is used by Clients to send Purchase Rewards (could have a discount)
        /// and Events to Gameball based on Events triggered by the Players Actions on the Client's Interface. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public ActionResponse SendAction(ActionRequest action)
        {
            action.Validate();
            action.PointsTransaction.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(action.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(action.PointsTransaction.TransactionTime), GameballUtils.ToValidAmount(action.PointsTransaction.RewardAmount));
            action.PointsTransaction.Hash = hash;
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Action, new StringContent(action.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<ActionResponse>(response.Content.ReadAsStringAsync().Result);

        }
        /// <summary>
        /// This Async service is used by Clients to send Purchase Rewards (could have a discount)
        /// and Events to Gameball based on Events triggered by the Players Actions on the Client's Interface. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<ActionResponse> SendActionAsync(ActionRequest action)
        {
            action.Validate();
            action.PointsTransaction.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(action.PlayerUniqueId, TransactionKey, GameballUtils.ToUtcTime(action.PointsTransaction.TransactionTime), GameballUtils.ToValidAmount(action.PointsTransaction.RewardAmount));
            action.PointsTransaction.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Action, new StringContent(action.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<ActionResponse>(await response.Content.ReadAsStringAsync());

        }
        /// <summary>
        /// This service For Creating a Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public CreateCouponResponse CreateDiscountCoupon(CreateCouponRequest coupon)
        {
            coupon.Validate();
            coupon.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(coupon.PlayerUniqueId, TransactionKey);
            coupon.Hash = hash;
            var response = this.Client.PostAsync(ApiBase + GameballConstants.Coupon, new StringContent(coupon.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<CreateCouponResponse>(response.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// This Async service For Creating a Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<CreateCouponResponse> CreateDiscountCouponAsync(CreateCouponRequest coupon)
        {
            coupon.Validate();
            coupon.TransactionTime = DateTime.UtcNow;
            string hash = GameballUtils.GetSHA1(coupon.PlayerUniqueId, TransactionKey);
            coupon.Hash = hash;
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.Coupon, new StringContent(coupon.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<CreateCouponResponse>(await response.Content.ReadAsStringAsync());
        }
        /// <summary>
        /// This service For Redeem Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public bool RedeemDiscountCoupon(string playerUniqueId, string code)
        {
            RedeemCouponRequest Coupon = new RedeemCouponRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey),
                TransactionTime = DateTime.UtcNow,
                Code = code
            };
            Coupon.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.RedeemDiscount, new StringContent(Coupon.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// This Async service For Redeem Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<bool> RedeemDiscountCouponAsync(string playerUniqueId, string code)
        {
            RedeemCouponRequest Coupon = new RedeemCouponRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey),
                TransactionTime = DateTime.UtcNow,
                Code = code
            };
            Coupon.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.RedeemDiscount, new StringContent(Coupon.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// This service For Validate Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public ValidateCouponResponse ValidateDiscountCoupon(string playerUniqueId, string code)
        {
            ValidateCouponRequest Coupon = new ValidateCouponRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey),
                TransactionTime = DateTime.UtcNow,
                Code = code
            };
            Coupon.Validate();
            var response = this.Client.PostAsync(ApiBase + GameballConstants.ValidateDiscount, new StringContent(Coupon.Serialize(), Encoding.UTF8, "application/json")).Result;
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<ValidateCouponResponse>(response.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// This async service For Validate Discount Coupon. 
        /// </summary>
        /// <exception cref="GameballException">Thrown if the request fails..</exception>
        public async Task<ValidateCouponResponse> ValidateDiscountCouponAsync(string playerUniqueId, string code)
        {
            ValidateCouponRequest Coupon = new ValidateCouponRequest()
            {
                PlayerUniqueId = playerUniqueId,
                Hash = GameballUtils.GetSHA1(playerUniqueId, TransactionKey),
                TransactionTime = DateTime.UtcNow,
                Code = code
            };
            Coupon.Validate();
            var response = await this.Client.PostAsync(ApiBase + GameballConstants.ValidateDiscount, new StringContent(Coupon.Serialize(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw (BuildGameballException(response));
            else
                return JsonConvert.DeserializeObject<ValidateCouponResponse>(await response.Content.ReadAsStringAsync());
        }

    }
}