using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface ICinemaBookingRepository : IBaseRepository<CinemaBooking>
    {
    }
    
    public class CinemaBookingRepository : BaseRepository<CinemaBooking>, ICinemaBookingRepository
    {
        public CinemaBookingRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
