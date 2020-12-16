using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Repositories
{
    public class ShuttleRepo : IShuttleRepo
    {
        private readonly DataContext.DataContext _dataContext;

        public ShuttleRepo(DataContext.DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Shuttle> GetShuttle(Guid id)
        {
            return await _dataContext.Shuttles.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<List<Shuttle>> GetAllShuttles()
        {
            return await Task.Run(() => _dataContext.Shuttles.ToListAsync());
        }

        public async Task<Shuttle> CreateShuttle(Shuttle inputShuttle)
        {
            return await Task.Run(async () =>
            {
                await _dataContext.Shuttles.AddAsync(inputShuttle);
                await _dataContext.SaveChangesAsync();
                return inputShuttle;
            });
        }

        public async Task<Shuttle> DeleteShuttle(Guid id)
        {
            var toDeleteShuttle = await _dataContext.Shuttles.FirstOrDefaultAsync(t => t.Id.Equals(id));
            if(toDeleteShuttle is null)
                throw new RepositoryException("Shuttle is not in the repository.");
            _dataContext.Shuttles.Remove(toDeleteShuttle);
            await _dataContext.SaveChangesAsync();
            return toDeleteShuttle;
        }

        public async Task<Shuttle> UpdateShuttle(Shuttle inputShuttle)
        {
            var updatedShuttle = await Task.Run(() =>
            {
                return _dataContext.Shuttles.Where(b => b.Id.Equals(inputShuttle.Id)).ToList().Select(s =>
                {
                    // TODO: add update by reflection
                    s.Name = inputShuttle.Name;
                    s.MaxCrewCapacity = inputShuttle.MaxCrewCapacity;
                    s.TeamOfExplorers = inputShuttle.TeamOfExplorers;
                    return s;
                }).First();
            });
            await _dataContext.SaveChangesAsync();
            return updatedShuttle;
        }
    }
}