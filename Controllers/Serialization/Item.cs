// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

namespace aspmarvel.Controllers.Serialization
{
    public class Item
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
}
