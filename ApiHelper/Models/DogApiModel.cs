using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiHelper.Models
{
    public class DogApiModel
    {
        [JsonProperty("message")]
        public List<string> Pictures { get; set; }


    }
}
