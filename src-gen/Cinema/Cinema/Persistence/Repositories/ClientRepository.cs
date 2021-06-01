using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
    }
    
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
