using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AuthDemoBot
{
    [Serializable]
    public class AuthenticationResponse
    {
        public object errors { get; set; }
        public int duration { get; set; }
        public string sessionId { get; set; }
        public int profileId { get; set; }
        public string countryCode { get; set; }
        public Oauth[] oauths { get; set; }
        public object deauthorizedDeviceName { get; set; }
        public Usersubscription userSubscription { get; set; }
        public string accountType { get; set; }
        public object currentTime { get; set; }
        public object firstError { get; set; }
    }

    [Serializable]
    public class Usersubscription
    {
        public object errors { get; set; }
        public object duration { get; set; }
        public object subscriptions { get; set; }
        public object nextBillDate { get; set; }
        public bool validForRadioStreaming { get; set; }
        public bool validForPremiumStreaming { get; set; }
        public object paymentType { get; set; }
        public object lastBillingMessage { get; set; }
        public object recurringPaymentId { get; set; }
        public bool failedPayment { get; set; }
        public long endDate { get; set; }
        public string accountType { get; set; }
        public int hoursTillEndDate { get; set; }
        public int subscriptionId { get; set; }
        public bool activeStreamer { get; set; }
        public object firstError { get; set; }
    }

    [Serializable]
    public class Oauth
    {
        public string oauthUuid { get; set; }
        public string type { get; set; }
    }


    [Serializable]
    public class ForYouResponse
    {
        public int duration { get; set; }
        public int profileId { get; set; }
        public Value[] values { get; set; }
    }

    [Serializable]
    public class Value
    {
        public int contentId { get; set; }
        public string label { get; set; }
        public string subLabel { get; set; }
        public string subType { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
        public string link { get; set; }
        public string deviceLink { get; set; }
        public string imagePath { get; set; }
        public Basedon basedOn { get; set; }
        public Content content { get; set; }
    }

    [Serializable]
    public class Basedon
    {
        public string _112 { get; set; }
        public string _31501796 { get; set; }
        public string _33221 { get; set; }
        public string _1469 { get; set; }
        public string _33218 { get; set; }
        public string _7772 { get; set; }
        public string _42340 { get; set; }
        public string _633 { get; set; }
        public string _16 { get; set; }
    }

    [Serializable]
    public class Content
    {
        public string name { get; set; }
        public string responseType { get; set; }
        public string stationId { get; set; }
        public int id { get; set; }
        public float score { get; set; }
        public string description { get; set; }
        public string band { get; set; }
        public string callLetters { get; set; }
        public string logo { get; set; }
        public string freq { get; set; }
        public Streams streams { get; set; }
        public bool isActive { get; set; }
        public string modified { get; set; }
        public Genre[] genres { get; set; }
        public string esid { get; set; }
        public Feeds feeds { get; set; }
        public string format { get; set; }
        public string provider { get; set; }
        public string rds { get; set; }
        public string website { get; set; }
        public Social social { get; set; }
        public string imagePath { get; set; }
        public string[] roviImages { get; set; }
    }

    [Serializable]
    public class Streams
    {
        public string hls_stream { get; set; }
        public string shoutcast_stream { get; set; }
        public string secure_rtmp_stream { get; set; }
        public string secure_hls_stream { get; set; }
        public string secure_shoutcast_stream { get; set; }
    }

    [Serializable]
    public class Feeds
    {
        public string site_id { get; set; }
        public string feed { get; set; }
    }

    [Serializable]
    public class Social
    {
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
    }

    [Serializable]
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public int sortIndex { get; set; }
        public bool primary { get; set; }
    }


    [Serializable]
    public class LocationInfo
    {
        public int duration { get; set; }
        public int total { get; set; }
        public Hit[] hits { get; set; }
    }

    [Serializable]
    public class Hit
    {
        public string name { get; set; }
        public int marketId { get; set; }
        public Loc loc { get; set; }
        public int stationCount { get; set; }
        public string stateAbbreviation { get; set; }
        public string stateId { get; set; }
        public string stateName { get; set; }
        public string city { get; set; }
        public string countryName { get; set; }
        public string countryId { get; set; }
        public string countryAbbreviation { get; set; }
    }

    [Serializable]
    public class Loc
    {
        public float lat { get; set; }
        public float lon { get; set; }
    }


    [Serializable]
    public class RadioStationList
    {
        public int duration { get; set; }
        public int total { get; set; }
        public HitStation[] hits { get; set; }
    }

    [Serializable]
    public class HitStation
    {
        public int id { get; set; }
        public float score { get; set; }
        public string name { get; set; }
        public string responseType { get; set; }
        public string description { get; set; }
        public string band { get; set; }
        public string callLetters { get; set; }
        public string logo { get; set; }
        public string freq { get; set; }
        public int cume { get; set; }
        public string countries { get; set; }
        public Streams streams { get; set; }
        public bool isActive { get; set; }
        public string modified { get; set; }
        public Market[] markets { get; set; }
        public Genre[] genres { get; set; }
        public string esid { get; set; }
        public Feeds feeds { get; set; }
        public string format { get; set; }
        public string provider { get; set; }
        public string rds { get; set; }
        public string website { get; set; }
        public Social social { get; set; }
        public string callLetterAlias { get; set; }
        public string callLetterRoyalty { get; set; }
    }

    [Serializable]
    public class Market
    {
        public string name { get; set; }
        public string marketId { get; set; }
        public int sortIndex { get; set; }
        public string city { get; set; }
        public int stateId { get; set; }
        public string stateAbbreviation { get; set; }
        public int cityId { get; set; }
        public string country { get; set; }
        public int countryId { get; set; }
        public bool origin { get; set; }
        public bool primary { get; set; }
    }

    public class IhrHelper
    {
        static string baseUrl = "https://api2.iheart.com/api/v2/";

        public static async Task<AuthenticationResponse> LoginToIHR()
        {
            string postData = string.Format("apiKey={0}&userName={1}&password={2}&deviceId={3}&deviceName={4}&host={5}",
                WebUtility.UrlEncode("YW5kcm9pZHwzfGpzb258NC4wMnxXaW44YjI="),
                WebUtility.UrlEncode("jzole@hotmail.com"),
                WebUtility.UrlEncode("test123"),
                "b96c84df-8f58-43a3-9b94-9489f433833c",
                WebUtility.UrlEncode("win10"),
                "win10.mobile.us");

            HttpWebRequest webReq = HttpWebRequest.Create("https://api2.iheart.com/api/v1/account/login") as HttpWebRequest;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(postData);
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentLength = data.Length;
            Stream requestStream = webReq.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)await webReq.GetResponseAsync();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();

            var result = JsonConvert.DeserializeObject<AuthenticationResponse>(pageContent);
            return result;
        }

        public static async Task<RadioStationList> GetRadioStations()
        {
            var radioUrl = "content/markets?apikey={0}&zipCode={1}";
            var recommendedUrl = baseUrl + string.Format(radioUrl,
            "YW5kcm9pZHwzfGpzb258NC4wMnxXaW44YjI=", 89101.ToString());
            var radioContent = await GetRequestContent(recommendedUrl);
            var result = JsonConvert.DeserializeObject<LocationInfo>(radioContent);
            string LiveStationsUrl = "content/liveStations?apikey={0}&limit={1}&offset={2}";
            var radioStationUrl = baseUrl + string.Format(LiveStationsUrl,
            "YW5kcm9pZHwzfGpzb258NC4wMnxXaW44YjI=", 10, 0) + string.Format("&marketId={0}", result.hits[0].marketId);
            var radioStationContent = await GetRequestContent(radioStationUrl);
            return JsonConvert.DeserializeObject<RadioStationList>(radioStationContent);
        }

        public static  string GetFitImage(string stationType, string Id)
        {
           // var radioUrl = "https://iscale.iheart.com/catalog/Artist/7948/?ops=fit(275,275)";
            var radioUrl = String.Format("https://iscale.iheart.com/catalog/{0}/{1}/?ops=fit(275,275)",stationType,Id);
            //var recommendedUrl = baseUrl + string.Format(radioUrl,
            //"YW5kcm9pZHwzfGpzb258NC4wMnxXaW44YjI=", 89101.ToString());
            //var radioContent = await GetRequestContent(radioUrl);
            //var result = JsonConvert.DeserializeObject<LocationInfo>(radioContent);
            ////  string LiveStationsUrl = "content/liveStations?apikey={0}&limit={1}&offset={2}";
            //  var radioStationUrl = baseUrl + string.Format(LiveStationsUrl,
            //   "YW5kcm9pZHwzfGpzb258NC4wMnxXaW44YjI=", 10, 0) + string.Format("&marketId={0}", result.hits[0].marketId);
            //  var radioStationContent = await GetRequestContent(radioStationUrl);
            //return JsonConvert.DeserializeObject<RadioStationList>(radioContent);
            return radioUrl;
        }

        public static async Task<ForYouResponse> GetForYou(AuthenticationResponse authResponse)
        {
            var recommendedUrl = baseUrl + string.Format("recs/{0}/?profileId={0}&X-User-Id={0}&X-Session-Id={1}&X-hostName={2}&offset={3}&limit={4}&campaignId=foryou_favorites",
                        authResponse.profileId,
                        WebUtility.UrlEncode(authResponse.sessionId),
                        "win10.mobile.us",
                        0,
                        10);

            string pageContent = await GetRequestContent(recommendedUrl);
            var result = JsonConvert.DeserializeObject<ForYouResponse>(pageContent);
            return result;
        }

        private static async Task<string> GetRequestContent(string recommendedUrl)
        {
            HttpWebRequest webReq = HttpWebRequest.Create(recommendedUrl) as HttpWebRequest;
            webReq.Method = "GET";

            HttpWebResponse myHttpWebResponse = (HttpWebResponse)await webReq.GetResponseAsync();

            Stream responseStream = myHttpWebResponse.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);

            string pageContent = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            responseStream.Close();

            myHttpWebResponse.Close();
            return pageContent;
        }
    }
}