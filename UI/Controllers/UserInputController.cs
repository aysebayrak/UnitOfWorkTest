using ApiLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Text;


namespace UI.Controllers
{
    public class UserInputController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public UserInputController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory= httpClientFactory;
        }

        public IActionResult AddNumber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNumber(string numbers)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numbers))
                {
                    return BadRequest("Geçerli veri girişi sağla.");
                }

                List<int> numberList = numbers.Split(',').Select(int.Parse).ToList();

                var client = new HttpClient();
                var responseMessage = await client.PostAsJsonAsync<List<int>>("https://localhost:7024/api/UserInput/addNumbers", numberList);


                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("AddNumber");
                }
                else
                {
                    ViewBag.ErrorMessage = "API'den hata aldınız.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Bir hata oluştu: {ex.Message}";
                return View();
            }


        }

        [Authorize(Roles = "Admin")]
        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7024/api/UserInput");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<NumberInputModel>>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}





