﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.General
{
    public sealed class ApiWebClient
    {
        private static readonly Encoding Encoding = Encoding.ASCII;
        private static readonly JsonSerializer JsonSerializer = new JsonSerializer {NullValueHandling = NullValueHandling.Ignore};

        private Authenticator authenticator;

        public ApiWebClient(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        private string BaseUrl { get; }

        public Authenticator Authenticator
        {
            private get { return authenticator; }

            set
            {
                authenticator = value;
                Encryptor.Key = Encoding.GetBytes(value.PrivateKey);
            }
        }

        public HMACSHA512 Encryptor { get; set; } = new HMACSHA512();

        public T GetData<T>(string command, params object[] parameters)
        {
            var relativeUrl = CreateRelativeUrl(command, parameters);

            var jsonString = QueryString(relativeUrl);
            var output = JsonSerializer.DeserializeObject<T>(jsonString);

            return output;
        }

        public T PostData<T>(string command, Dictionary<string, object> postData) where T : new()
        {
            var jsonString = string.Empty;
            try
            {
                postData.Add("command", command);
                postData.Add("nonce", Helper.GetCurrentHttpPostNonce());
                jsonString = PostString(Helper.ApiUrlHttpsRelativeTrading, postData.ToHttpPostString());

                if (jsonString == "[]" || jsonString == "{}" || string.IsNullOrEmpty(jsonString))
                {
                    return new T();
                }

                var output = JsonSerializer.DeserializeObject<T>(jsonString);
                return output;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    throw new Exception("Error. Json String: " + jsonString + "." + Environment.NewLine + ex);
                }
                throw;
            }
        }

        private string QueryString(string relativeUrl)
        {
            var request = CreateHttpWebRequest("GET", relativeUrl);

            return request.GetResponseString();
        }

        private string PostString(string relativeUrl, string postData)
        {
            var request = CreateHttpWebRequest("POST", relativeUrl);
            request.ContentType = "application/x-www-form-urlencoded";

            var postBytes = Encoding.GetBytes(postData);
            request.ContentLength = postBytes.Length;

            request.Headers["Key"] = Authenticator.PublicKey;
            request.Headers["Sign"] = Encryptor.ComputeHash(postBytes).ToStringHex();

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(postBytes, 0, postBytes.Length);
            }

            return request.GetResponseString();
        }

        private static string CreateRelativeUrl(string command, object[] parameters)
        {
            var relativeUrl = command;
            if (parameters.Length != 0)
            {
                relativeUrl += "&" + string.Join("&", parameters);
            }

            return relativeUrl;
        }

        private HttpWebRequest CreateHttpWebRequest(string method, string relativeUrl)
        {
            var request = WebRequest.CreateHttp(BaseUrl + relativeUrl);
            request.Method = method;
            request.UserAgent = "Poloniex API .NET v" + Helper.AssemblyVersionString;

            request.Timeout = Timeout.Infinite;

            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return request;
        }
    }
}