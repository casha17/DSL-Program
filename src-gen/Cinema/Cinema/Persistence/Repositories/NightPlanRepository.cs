using System;
using Cinema.Configuration;
using Cinema.Persistence.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinema.Persistence.Repositories
{
    public interface INightPlanRepository : IBaseRepository<NightPlan>
    {
    }
    
    public class NightPlanRepository : BaseRepository<NightPlan>, INightPlanRepository
    {
        public NightPlanRepository(IMongoClient client, IOptions<PersistenceConfiguration> config) : base(client, config)
        {
        }
    }
}
