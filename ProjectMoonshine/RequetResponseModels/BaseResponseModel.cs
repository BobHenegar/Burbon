using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectMoonshine.RequetResponseModels
{
    public class BaseResponseModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
    public class BaseResponseModel<T> : BaseResponseModel
    {
        public T Data { get; set; }
    }
}
