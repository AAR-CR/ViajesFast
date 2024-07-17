using API_Vuelos.Data;
using API_Vuelos.Models;

namespace API_Vuelos.Services
{
    public class VueloService
    {
        private readonly ViajesFastDbConext _context;

        public VueloService(ViajesFastDbConext context)
        {
            _context = context;
        }

        public List<Vuelo> GetVuelos()
        {
            return _context.Vuelos.ToList();
        }

        public Vuelo GetVueloById(int id)
        {
            return _context.Vuelos.Find(id);
        }

        public Vuelo AddVuelo(Vuelo vuelo) 
        {
            _context.Vuelos.Add(vuelo);
            _context.SaveChanges();
            return vuelo;
        }
    }

}
