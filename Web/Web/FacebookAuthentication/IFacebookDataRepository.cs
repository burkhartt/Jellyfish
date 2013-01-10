using System;
using System.Web;

namespace Web.FacebookAuthentication {
    public interface IFacebookDataRepository {
        string GenerateNewStateId();
        void SaveUserAuthenticationCode(string code);
        string GetUserAuthenticationCode();
        string GetStateId();
        void SaveAccessToken(string accessToken);
        string GetAccessToken();
    }

    public class FacebookStateRepository : IFacebookDataRepository {
        public string GenerateNewStateId() {
            var generateNewId = Guid.NewGuid().ToString();
            HttpContext.Current.Session["FacebookStateId"] = generateNewId;
            return generateNewId;
        }

        public void SaveUserAuthenticationCode(string code) {
            HttpContext.Current.Session["FacebookUserCode"] = code;
        }

        public string GetUserAuthenticationCode() {
            return (string)HttpContext.Current.Session["FacebookUserCode"];
        }

        public string GetStateId() {
            return (string)HttpContext.Current.Session["FacebookStateId"];
        }

        public void SaveAccessToken(string accessToken) {
            HttpContext.Current.Session["FacebookAccessToken"] = accessToken;
        }

        public string GetAccessToken() {
            return (string)HttpContext.Current.Session["FacebookAccessToken"];
        }
    }
}