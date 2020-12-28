using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;
using ProjectAstra.Web.CrewApi.Infrastructure.Extensions;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Repositories
{
    public class ShuttleRepository : IShuttleRepository
    {
        private readonly DataContext _dataContext;

        public ShuttleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Shuttle>> GetAllShuttles(ShuttleFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.Shuttles.AsQueryable()).Skip(skip).Take(pagination).ToListAsync();
        }

        public async Task<bool> CreateShuttle(Shuttle inputShuttle)
        {
            await _dataContext.Shuttles.AddAsync(inputShuttle);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShuttle(Guid id)
        {
            _dataContext.Shuttles.Remove(
                await _dataContext.Shuttles.FirstOrDefaultAsync(shuttle => shuttle.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            var shuttleToUpdate =
                (await _dataContext.Shuttles.FirstOrDefaultAsync(shuttle => shuttle.Id.Equals(inputShuttle.Id)));
            shuttleToUpdate.UpdateByReflection(inputShuttle);
            await _dataContext.SaveChangesAsync();
            return shuttleToUpdate;
        }
    }
}