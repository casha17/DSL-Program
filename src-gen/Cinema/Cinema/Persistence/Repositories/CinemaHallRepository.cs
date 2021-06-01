using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface ICinemaHallRepository : IBaseRepository<CinemaHall>
    {
    }
    
    public class CinemaHallRepository : BaseRepository<CinemaHall>, ICinemaHallRepository
    {
        public CinemaHallRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
