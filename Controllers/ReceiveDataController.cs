using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ReceiveData.Controllers
{
    public class ReceiveDataController : Controller
    {
        //ref https://www.compilemode.com/2020/12/get-data-in-aspnet-mvc-using-web-api.html
        string url = "";

        List<JObject> jsResult = new List<JObject>();

        public async Task<ActionResult> Index()
        {
            using( var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/");

                if(res.IsSuccessStatusCode)
                {
                    var resp = res.Content.ReadAsStringAsync().Result;
                    jsResult = JsonConvert.DeserializeObject<List<JObject>>(resp);
                }

                return View(jsResult);

            }

        }
    }
}