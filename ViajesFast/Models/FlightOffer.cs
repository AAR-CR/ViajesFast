using System;
using System.Collections.Generic;

namespace ViajesFast.Models
{
    public class FlightOffersResponse
    {
        public Meta Meta { get; set; }
        public List<FlightOffer> Data { get; set; }
    }

    public class Meta
    {
        public int Count { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public string Self { get; set; }
    }

    public class FlightOffer
    {
        public string Id { get; set; }
        public Price Price { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public int NumberOfBookableSeats { get; set; }
        public string LastTicketingDate { get; set; }
    }

    public class Price
    {
        public string Currency { get; set; }
        public string GrandTotal { get; set; }
    }

    public class Itinerary
    {
        public string Duration { get; set; }
        public List<Segment> Segments { get; set; }
    }

    public class Segment
    {
        public Location Departure { get; set; }
        public Location Arrival { get; set; }
        public string CarrierCode { get; set; }
        public string AircraftCode { get; set; }
    }

    public class Location
    {
        public string IataCode { get; set; }
        public DateTime At { get; set; }
    }
}
