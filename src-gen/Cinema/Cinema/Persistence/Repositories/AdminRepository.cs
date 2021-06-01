using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
    }
    
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
