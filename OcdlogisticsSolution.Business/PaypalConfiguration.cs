using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OcdlogisticsSolution.Business
{
    public static class PaypalConfiguration
    {
        //Variables for storing the clientID and clientSecret key  
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        //Constructor  
        static PaypalConfiguration()
        
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }
        // getting properties from the web.config  
        public static Dictionary<string, string> GetConfig()
        {
            /* PayPal.Api.ConfigManager.Instance.GetProperties();*/
            return new Dictionary<string, string>() {
                { "clientId", "AXlb_OtNrW_3d7wWVMJsp4oPchptaa5-L-8PAqjBKP1RWWTJxWuEpPMIVVvsd4Jb2xC7Kas9PqRl5dEl" },
                { "clientSecret", "ELehBbGvWJsK5dHliWbHW352GQdg11XrdOmPUqaw6W5ROU3Bkba1rhx-mSaiUvRj6s-lgmRxRu8OaMjS" }
            };
            

        }
        private static string GetAccessToken()
        {
            // getting accesstocken from paypal  
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken  
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}