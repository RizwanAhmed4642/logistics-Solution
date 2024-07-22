using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Web.Util
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = gettconfig();
            clientId = "";
            clientSecret = "";
        }
        private static Dictionary<string,string> gettconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, gettconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetApiContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = gettconfig();
            return apicontext;

        }
    }
}