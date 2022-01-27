using aspmarvel.Controllers.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace aspmarvel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarvelController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ILogger<MarvelController> _logger;
        List<String> nomePersonagem = new List<string>();

        public MarvelController(ILogger<MarvelController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get()
        {
            var t = Process();
            return t.ToString();
        }


        public static async Task<JToken> Process()
        {

            var test = client.GetStringAsync("https://gateway.marvel.com/v1/public/characters?ts=1642539689&apikey=89737e2b5be01e946ade4173055a5522&hash=2cd7277ab38669204ee5f5758d7e8037&limit=20");
            var k = await test;
            JObject json = JObject.Parse(k);
            var result = json.GetValue("data");

            dynamic results = JsonConvert.DeserializeObject<dynamic>(k);
            var data = results.data;

            var limit = data.GetValue("limit");

            var personagens = data.results;

            var personagem1 = personagens[0];
            var personagem2 = personagens[1];


            List<Result> listResult = new List<Result>();
            foreach (var persongem in personagens)
            {
                Result result1 = new Result();
                result1.id = persongem.GetValue("id");
                listResult.Add(result1);
            }


            var js = new DataContractJsonSerializer(typeof(List<Result>));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(personagens));
            var character = (List<Result>)js.ReadObject(personagens);

            return result;
        }

    }
}


