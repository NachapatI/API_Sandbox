using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using API_Sandbox.Model;


namespace API_Sandbox.Controllers;

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    List<ReceiveData> lstResult = new List<ReceiveData>();

    // public IActionResult Index()
     public async Task<IActionResult> Index()
    {
        string dt = DateTime.Now.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
        string url = @"https://apigw1.bot.or.th";
        string apiUrl = @"/bot/public/Stat-ReferenceRate/v2/DAILY_REF_RATE/";
        apiUrl += String.Format("?start_period={0}&end_period={0}",dt);
        
        // Console.WriteLine("-"+url);
        using( var client = new HttpClient())
        {
            client.BaseAddress = new System.Uri(url);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-IBM-Client-Id", "CONFIG_ID");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage res = await client.GetAsync(apiUrl);

            // Console.WriteLine("-RequestAPI");
            if(res.IsSuccessStatusCode)
            {
                var resp = res.Content.ReadAsStringAsync().Result;
                // Console.WriteLine("-Get resp:"+resp.ToString());
                lstResult.Add(new ReceiveData() {Key = lstResult.Count, 
                Value = JObject.Parse(resp.ToString())});
                //  = JsonConvert.DeserializeObject<JObject>(resp);
            }
            else
            {
                lstResult = new List<ReceiveData>();
                ModelState.AddModelError(string.Empty, "Get Data Failure.");
            }

            return View(lstResult);

        }

        // return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
