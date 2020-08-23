namespace Gameball.Utils
{
    using System;
    using System.Text;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Security.Cryptography;
    using System.Linq;
    using global::Gameball.Exceptions;
    using System.Net;
    using global::Gameball.Constants;

    /// <summary>
    /// Gameball Utilities class, supports algorithms used in issuing Gameball requests and performing necessary checks and validations.
    /// </summary>
    internal static class GameballUtils
    {
        /// <summary>
        /// Used to calculate the bodyHash parameter in transactions
        /// </summary>
        /// <returns> a <see cref="string">string </see>representing the bodyHash.
        /// </returns>
        /// <exception cref="GameballException">Thrown if Transaction key is null.</exception>
        public static string GetSHA1(string playerUniqueId, string transactionKey , string transactionTime="",string Amount="")
        {
            if (Amount == null)
                Amount = "0.0";
            if (transactionKey == null)
                throw (new GameballException(HttpStatusCode.Unauthorized, "Transaction Key required for this request"));
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}",playerUniqueId,transactionTime,Amount,transactionKey)));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        /// <summary>
        /// gets UTC time now in the format yyMMddHHmmss
        /// </summary>
        public static string GetUtcTime()
        {
            //UTC Time
            return DateTime.UtcNow.ToString("yyMMddHHmmss");

        }

        /// <summary>
        /// converts passed time to UTC time in the format yyMMddHHmmss
        /// </summary>
        public static string ToUtcTime(DateTime Time)
        {
            //UTC Time
            return Time.ToUniversalTime().ToString("yyMMddHHmmss");

        }

        /// <summary>
        /// Checks if a string contains white space
        /// </summary>
        /// <param name="str"></param>
        public static bool ContainsWhitespace(string str)
        {
            Regex whitespaceRegex = new Regex(@"\s", RegexOptions.CultureInvariant);
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return whitespaceRegex.IsMatch(str);
        }

        /// <summary>
        /// Checks if a string is a valid email
        /// </summary>
        /// <param name="email"></param>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// converts passed amount into a valid string to calculate hash.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ToValidAmount(double amount)
        {
            if (amount == (int)amount)
                return (((int)amount).ToString() + ".0");
            else
                return (((double)amount).ToString());
        }

        /// <summary>
        /// converts GameballLang to corresponding code.
        /// </summary>
        /// <param name="Language"></param>
        /// <returns></returns>
        public static string ToValidLang(GameballLang Language)
        {
            switch (Language)
            {
                case GameballLang.English:
                    return "en";
                case GameballLang.Arabic:
                    return "ar";
                case GameballLang.French:
                    return "fr";
                case GameballLang.German:
                    return "de";
                case GameballLang.Italian:
                    return "it";
                case GameballLang.Polish:
                    return "pl";
                case GameballLang.Portuguese:
                    return "pt";
                case GameballLang.Spanish:
                    return "es";

                default:
                    return "en";
            }
        }

             

    }
}
