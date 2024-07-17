using API_Vuelos.Data;
using API_Vuelos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API_Vuelos.Services
{
    public class ReservaService
    {
        private readonly ViajesFastDbConext _context;

        public ReservaService(ViajesFastDbConext context)
        {
            _context = context;
        }

        public List<Reserva> GetReservas()
        {
            return _context.Reservas.Include(r => r.Usuario).Include(r => r.Vuelo).ToList();
        }

        public Reserva GetReservaById(int id)
        {
            return _context.Reservas.Include(r => r.Usuario).Include(r => r.Vuelo).FirstOrDefault(r => r.Id == id);
        }

        public Reserva AddReserva(Reserva reserva)
        {
            var usuario = _context.Usuarios.Find(reserva.UsuarioId);
            var vuelo = _context.Vuelos.Find(reserva.VueloId);

            if (usuario == null || vuelo == null)
            {
                return null;
            }

            reserva.Usuario = usuario;
            reserva.Vuelo = vuelo;
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
            return reserva;
        }
    }
}
