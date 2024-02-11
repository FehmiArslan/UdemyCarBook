using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using UdemyCarBookDto.LocationDtos;
using UdemyCarBookDto.ReservationnsDto;

namespace UdemyCarBook.WebUI.Controllers
{
    public class ReservationnController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ReservationnController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v1 = "Araç Kiralama";
            ViewBag.v2 = "Araç Rezervasyon Formu";
            ViewBag.v3 = id;


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7203/api/Locations");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);
            List<SelectListItem> values2 = (from x in values
                                            select new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.LocationID.ToString()
                                            }).ToList();

            ViewBag.v = values2;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationnDto createReservationnDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createReservationnDto);
                jsonData = Regex.Replace(jsonData, @"\\", "");
                StringContent stringContent = new StringContent(jsonData, Encoding.Unicode, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7203/api/Reservationns", stringContent);

                //https://localhost:7203/api/Reservationns
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Default");
                }
                return View();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
          
        }
    }
}
