using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using ViajesFast.Models;
using ViajesFast.Services;

namespace ViajesFast.Controllers
{                                                //ENTRADA A LA PÁGINA CON DATOS DE FINES ILUSTRATIVOS
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Vuelo> vuelosFictcio = new List<Vuelo>
            {
                new Vuelo { Id = 1, Destino = "Madrid", Fecha = DateTime.Now.AddDays(10), NumeroDePasajeros = 200, Precio = "150.00", Aerolinea = "Iberia" },
                new Vuelo { Id = 2, Destino = "Barcelona", Fecha = DateTime.Now.AddDays(15), NumeroDePasajeros = 150, Precio = "120.00", Aerolinea = "Vueling" },
                new Vuelo { Id = 3, Destino = "Paris", Fecha = DateTime.Now.AddDays(20), NumeroDePasajeros = 180, Precio = "200.00", Aerolinea = "Air France" },
                new Vuelo { Id = 4, Destino = "Nueva York", Fecha = DateTime.Now.AddDays(25), NumeroDePasajeros = 250, Precio = "300.00", Aerolinea = "Delta Airlines" },
                new Vuelo { Id = 5, Destino = "Tokio", Fecha = DateTime.Now.AddDays(30), NumeroDePasajeros = 220, Precio = "450.00", Aerolinea = "ANA" },
                new Vuelo { Id = 6, Destino = "Londres", Fecha = DateTime.Now.AddDays(35), NumeroDePasajeros = 175, Precio = "220.00", Aerolinea = "British Airways" },
                new Vuelo { Id = 7, Destino = "Roma", Fecha = DateTime.Now.AddDays(40), NumeroDePasajeros = 190, Precio = "180.00", Aerolinea = "Alitalia" },
                new Vuelo { Id = 8, Destino = "Berlín", Fecha = DateTime.Now.AddDays(45), NumeroDePasajeros = 160, Precio = "170.00", Aerolinea = "Lufthansa" },
                new Vuelo { Id = 9, Destino = "Sídney", Fecha = DateTime.Now.AddDays(50), NumeroDePasajeros = 300, Precio = "500.00", Aerolinea = "Qantas" },
                new Vuelo { Id = 10, Destino = "Buenos Aires", Fecha = DateTime.Now.AddDays(55), NumeroDePasajeros = 210, Precio = "350.00", Aerolinea = "Aerolineas Argentinas" },
                new Vuelo { Id = 11, Destino = "Toronto", Fecha = DateTime.Now.AddDays(60), NumeroDePasajeros = 240, Precio = "320.00", Aerolinea = "Air Canada" },
                new Vuelo { Id = 12, Destino = "Ciudad de México", Fecha = DateTime.Now.AddDays(65), NumeroDePasajeros = 230, Precio = "290.00", Aerolinea = "Aeroméxico" },
                new Vuelo { Id = 13, Destino = "Sao Paulo", Fecha = DateTime.Now.AddDays(70), NumeroDePasajeros = 200, Precio = "310.00", Aerolinea = "LATAM Airlines" },
                new Vuelo { Id = 14, Destino = "Los Ángeles", Fecha = DateTime.Now.AddDays(75), NumeroDePasajeros = 250, Precio = "340.00", Aerolinea = "American Airlines" },
                new Vuelo { Id = 15, Destino = "Chicago", Fecha = DateTime.Now.AddDays(80), NumeroDePasajeros = 180, Precio = "280.00", Aerolinea = "United Airlines" },
                new Vuelo { Id = 16, Destino = "Miami", Fecha = DateTime.Now.AddDays(85), NumeroDePasajeros = 220, Precio = "330.00", Aerolinea = "American Airlines" },
                new Vuelo { Id = 17, Destino = "San Francisco", Fecha = DateTime.Now.AddDays(90), NumeroDePasajeros = 210, Precio = "350.00", Aerolinea = "United Airlines" },
                new Vuelo { Id = 18, Destino = "Lima", Fecha = DateTime.Now.AddDays(95), NumeroDePasajeros = 190, Precio = "270.00", Aerolinea = "LATAM Airlines" },
                new Vuelo { Id = 19, Destino = "Bogotá", Fecha = DateTime.Now.AddDays(100), NumeroDePasajeros = 200, Precio = "290.00", Aerolinea = "Avianca" },
                new Vuelo { Id = 20, Destino = "Santiago", Fecha = DateTime.Now.AddDays(105), NumeroDePasajeros = 220, Precio = "310.00", Aerolinea = "LATAM Airlines" },
                new Vuelo { Id = 21, Destino = "Montevideo", Fecha = DateTime.Now.AddDays(110), NumeroDePasajeros = 180, Precio = "250.00", Aerolinea = "Aerolineas Argentinas" },
                new Vuelo { Id = 22, Destino = "Río de Janeiro", Fecha = DateTime.Now.AddDays(115), NumeroDePasajeros = 230, Precio = "330.00", Aerolinea = "GOL Linhas Aéreas" },
                new Vuelo { Id = 23, Destino = "Lisboa", Fecha = DateTime.Now.AddDays(120), NumeroDePasajeros = 210, Precio = "200.00", Aerolinea = "TAP Portugal" },
                new Vuelo { Id = 24, Destino = "Dublín", Fecha = DateTime.Now.AddDays(125), NumeroDePasajeros = 170, Precio = "240.00", Aerolinea = "Aer Lingus" },
                new Vuelo { Id = 25, Destino = "Amsterdam", Fecha = DateTime.Now.AddDays(130), NumeroDePasajeros = 190, Precio = "220.00", Aerolinea = "KLM" },
                new Vuelo { Id = 26, Destino = "Bruselas", Fecha = DateTime.Now.AddDays(135), NumeroDePasajeros = 160, Precio = "200.00", Aerolinea = "Brussels Airlines" },
                new Vuelo { Id = 27, Destino = "Viena", Fecha = DateTime.Now.AddDays(140), NumeroDePasajeros = 180, Precio = "230.00", Aerolinea = "Austrian Airlines" },
                new Vuelo { Id = 28, Destino = "Atenas", Fecha = DateTime.Now.AddDays(145), NumeroDePasajeros = 200, Precio = "250.00", Aerolinea = "Aegean Airlines" },
                new Vuelo { Id = 29, Destino = "Estocolmo", Fecha = DateTime.Now.AddDays(150), NumeroDePasajeros = 170, Precio = "210.00", Aerolinea = "SAS" },
                new Vuelo { Id = 30, Destino = "Copenhague", Fecha = DateTime.Now.AddDays(155), NumeroDePasajeros = 160, Precio = "220.00", Aerolinea = "SAS" },
                new Vuelo { Id = 31, Destino = "Oslo", Fecha = DateTime.Now.AddDays(160), NumeroDePasajeros = 150, Precio = "200.00", Aerolinea = "Norwegian Air" },
                new Vuelo { Id = 32, Destino = "Helsinki", Fecha = DateTime.Now.AddDays(165), NumeroDePasajeros = 180, Precio = "230.00", Aerolinea = "Finnair" },
                new Vuelo { Id = 33, Destino = "Varsovia", Fecha = DateTime.Now.AddDays(170), NumeroDePasajeros = 190, Precio = "220.00", Aerolinea = "LOT Polish Airlines" },
                new Vuelo { Id = 34, Destino = "Praga", Fecha = DateTime.Now.AddDays(175), NumeroDePasajeros = 200, Precio = "210.00", Aerolinea = "Czech Airlines" },
                new Vuelo { Id = 35, Destino = "Budapest", Fecha = DateTime.Now.AddDays(180), NumeroDePasajeros = 170, Precio = "240.00", Aerolinea = "Wizz Air" },
                new Vuelo { Id = 36, Destino = "Estambul", Fecha = DateTime.Now.AddDays(185), NumeroDePasajeros = 220, Precio = "250.00", Aerolinea = "Turkish Airlines" },
                new Vuelo { Id = 37, Destino = "Dubái", Fecha = DateTime.Now.AddDays(190), NumeroDePasajeros = 230, Precio = "350.00", Aerolinea = "Emirates" },
                new Vuelo { Id = 38, Destino = "Doha", Fecha = DateTime.Now.AddDays(195), NumeroDePasajeros = 210, Precio = "340.00", Aerolinea = "Qatar Airways" },
                new Vuelo { Id = 39, Destino = "Abu Dabi", Fecha = DateTime.Now.AddDays(200), NumeroDePasajeros = 200, Precio = "330.00", Aerolinea = "Etihad Airways" },
                new Vuelo { Id = 40, Destino = "Hong Kong", Fecha = DateTime.Now.AddDays(205), NumeroDePasajeros = 240, Precio = "400.00", Aerolinea = "Cathay Pacific" },
                new Vuelo { Id = 41, Destino = "Singapur", Fecha = DateTime.Now.AddDays(210), NumeroDePasajeros = 230, Precio = "420.00", Aerolinea = "Singapore Airlines" },
                new Vuelo { Id = 42, Destino = "Seúl", Fecha = DateTime.Now.AddDays(215), NumeroDePasajeros = 220, Precio = "380.00", Aerolinea = "Korean Air" },
                new Vuelo { Id = 43, Destino = "Bangkok", Fecha = DateTime.Now.AddDays(220), NumeroDePasajeros = 210, Precio = "360.00", Aerolinea = "Thai Airways" },
                new Vuelo { Id = 44, Destino = "Jakarta", Fecha = DateTime.Now.AddDays(225), NumeroDePasajeros = 200, Precio = "340.00", Aerolinea = "Garuda Indonesia" },
                new Vuelo { Id = 45, Destino = "Kuala Lumpur", Fecha = DateTime.Now.AddDays(230), NumeroDePasajeros = 190, Precio = "320.00", Aerolinea = "Malaysia Airlines" },
                new Vuelo { Id = 46, Destino = "Manila", Fecha = DateTime.Now.AddDays(235), NumeroDePasajeros = 180, Precio = "300.00", Aerolinea = "Philippine Airlines" },
                new Vuelo { Id = 47, Destino = "Ho Chi Minh City", Fecha = DateTime.Now.AddDays(240), NumeroDePasajeros = 170, Precio = "280.00", Aerolinea = "Vietnam Airlines" },
                new Vuelo { Id = 48, Destino = "Hanoi", Fecha = DateTime.Now.AddDays(245), NumeroDePasajeros = 160, Precio = "260.00", Aerolinea = "Vietnam Airlines" },
                new Vuelo { Id = 49, Destino = "Melbourne", Fecha = DateTime.Now.AddDays(250), NumeroDePasajeros = 150, Precio = "500.00", Aerolinea = "Qantas" },
                new Vuelo { Id = 50, Destino = "Auckland", Fecha = DateTime.Now.AddDays(255), NumeroDePasajeros = 140, Precio = "520.00", Aerolinea = "Air New Zealand" }
            };

            try
            {

                return View(vuelosFictcio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista completa de vuelos.");
                return RedirectToAction("Error", new { message = "No se pudieron obtener los datos de la API. Por favor, intente nuevamente más tarde." });
            }

        }


        [Authorize]
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
}
