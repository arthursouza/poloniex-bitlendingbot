using System.Net;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.General
{
    internal class JsonResponse<T>
    {
        private T data;

        [JsonProperty("status")]
        private string Status { get; set; }

        [JsonProperty("message")]
        private string Message { get; set; }

        [JsonProperty("data")]
        internal T Data
        {
            get { return data; }

            private set
            {
                CheckStatus();
                data = value;
            }
        }

        internal void CheckStatus()
        {
            if (Status != "success")
            {
                if (string.IsNullOrWhiteSpace(Message))
                {
                    throw new WebException("Could not parse data from the server.", WebExceptionStatus.UnknownError);
                }
                throw new WebException("Could not parse data from the server: " + Message, WebExceptionStatus.UnknownError);
            }
        }
    }
}