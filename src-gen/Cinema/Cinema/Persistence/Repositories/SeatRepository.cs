using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface ISeatRepository : IBaseRepository<Seat>
    {
    }
    
    public class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        public SeatRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
