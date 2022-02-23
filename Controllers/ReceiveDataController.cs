using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System;

namespace ReceiveData.Controllers
{
    public class ReceiveDataController : Controller
    {
        //ref https://www.compilemode.com/2020/12/get-data-in-aspnet-mvc-using-web-api.html

        List<JObject> jsResult = new List<JObject>();

        public async Task<ActionResult> Index()
        {
            string dt = DateTime.Now.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
            string url = @"https://apigw1.bot.or.th/bot/public/Stat-ReferenceRate/v2/MONTHLY_REF_RATE/";
            url += String.Format("?start_period={0}&end_period={0}",dt);

            using( var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-IBM-Client-Id", "9392a5e0-7969-485b-a7c7-46d3a04e8f3e");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("api/");

                if(res.IsSuccessStatusCode)
                {
                    var resp = res.Content.ReadAsStringAsync().Result;
                    jsResult = JsonConvert.DeserializeObject<List<JObject>>(resp);
                }
                else
                {
                    jsResult = new List<JObject>();
                    ModelState.AddModelError(string.Empty, "Get Data Failure.");
                }

                return View(jsResult);

            }

        }
    }
}