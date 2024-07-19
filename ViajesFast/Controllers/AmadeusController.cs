using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ViajesFast.Models;
using ViajesFast.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ViajesFast.Controllers
{
                                                  //SECCION DE MANEJO API DE TERCEROS
    public class AmadeusController : Controller
    {
        private readonly Dictionary<string, string> _airports = new Dictionary<string, string>
        {
            { "San José, Costa Rica (SJO)", "SJO" },
            { "Los Angeles, USA (LAX)", "LAX" },
            { "New York, USA (JFK)", "JFK" },
            { "London, UK (LHR)", "LHR" },
            { "Paris, France (CDG)", "CDG" },
            { "Tokyo, Japan (HND)", "HND" },
            { "Sydney, Australia (SYD)", "SYD" },
            { "Toronto, Canada (YYZ)", "YYZ" },
            { "Mexico City, Mexico (MEX)", "MEX" },
            { "São Paulo, Brazil (GRU)", "GRU" },
            { "Dubai, UAE (DXB)", "DXB" },
            { "Hong Kong, China (HKG)", "HKG" },
            { "Beijing, China (PEK)", "PEK" },
            { "Madrid, Spain (MAD)", "MAD" },
            { "Berlin, Germany (BER)", "BER" },
            { "Rome, Italy (FCO)", "FCO" },
            { "Amsterdam, Netherlands (AMS)", "AMS" },
            { "Vienna, Austria (VIE)", "VIE" },
            { "Zurich, Switzerland (ZRH)", "ZRH" },
            { "Moscow, Russia (SVO)", "SVO" },
            { "Cape Town, South Africa (CPT)", "CPT" },
            { "Buenos Aires, Argentina (EZE)", "EZE" },
            { "Lima, Peru (LIM)", "LIM" },
            { "Santiago, Chile (SCL)", "SCL" },
            { "Bogotá, Colombia (BOG)", "BOG" },
            { "Lisbon, Portugal (LIS)", "LIS" },
            { "Athens, Greece (ATH)", "ATH" },
            { "Bangkok, Thailand (BKK)", "BKK" },
            { "Seoul, South Korea (ICN)", "ICN" },
            { "Singapore (SIN)", "SIN" }
            // No son todas .....
        };

        [Authorize]
        public async Task<IActionResult> Index(string origin = "SJO", string destination = "LAX", int adults = 1)
        {
            string departureDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.Airports = _airports;

            try
            {
                string flightOffersJson = await AmadeusService.GetFlightOffers(origin, destination, departureDate, adults);

                var flightOffersResponse = JsonConvert.DeserializeObject<FlightOffersResponse>(flightOffersJson);

                string originAirport = GetAirportName(origin);
                string destinationAirport = GetAirportName(destination);

                // Pasar nombres de aeropuertos a la vista
                ViewBag.Origin = originAirport;
                ViewBag.Destiny = destinationAirport;

                return View(flightOffersResponse.Data); // Pasar la lista de FlightOffer a la vista
            }
            catch (HttpRequestException ex)
            {
                ViewBag.ErrorMessage = $"Error al obtener ofertas de vuelo: {ex.Message}";
                return View(new List<FlightOffer>());

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Ocurrió un error: {ex.Message}";
                return View(new List<FlightOffer>());
            }

        }

        private string GetAirportName(string code)
        {
            var airport = _airports.FirstOrDefault(a => a.Value == code).Key;
            return !string.IsNullOrEmpty(airport) ? airport : "Código no encontrado";
        }

        [HttpPost]
        public IActionResult Reserve(string Id, string PriceGrandTotal, string PriceCurrency, string Duration, int NumberOfBookableSeats, string LastTicketingDate, string Destination, DateTime DepartureDate, string CarrierCode, string AircraftCode)
        {
            //Del flightoffer a vuelo de modelos locales
            var vuelo = new Vuelo
            {
                // Id = Id, 
                Destino = Destination,
                Fecha = DepartureDate,
                NumeroDePasajeros = NumberOfBookableSeats,
                Precio = PriceGrandTotal,
                Aerolinea = CarrierCode
            };

            // Redirigir a la vista de carrito de compras con la información del vuelo
            return RedirectToAction("Cart", "Vuelo", vuelo);
        }
    }
}
