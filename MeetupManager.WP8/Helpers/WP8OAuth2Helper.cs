using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeetupManager.WP8.Helpers
{
    public class WP8OAuth2Helper
    {
        private string _clientId;
        private string _clientSecret;
        private Uri _authUrl;
        private Uri _redirectUrl;
        private Uri _accessTokenUrl;

        public WP8OAuth2Helper(string clientId, string clientSecret, Uri authorizeUrl, Uri redirectUrl, Uri accessTokenUrl)
        {
            this._clientId = clientId;
            this._clientSecret = clientSecret;
            this._authUrl = authorizeUrl;
            this._redirectUrl = redirectUrl;
            this._accessTokenUrl = accessTokenUrl;
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public void GetRequestToken()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(this._authUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request);
        }

        private void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;

            Stream postStream = request.EndGetRequestStream(callbackResult);

            string postData = string.Format("consumer_key=consumerKey&redirect_uri={0}", this._redirectUrl);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            request.BeginGetResponse(new AsyncCallback(GetResponseStreamCallback), request);
        }

        private void GetResponseStreamCallback(IAsyncResult callbackResult)
        {
            HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);

            
        }

        public void GetAccessToken()
        {

        }        
    }
}
