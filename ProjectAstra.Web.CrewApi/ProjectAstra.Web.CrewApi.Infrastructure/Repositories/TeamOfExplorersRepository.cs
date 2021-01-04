using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Repositories
{
    public class TeamOfExplorersRepository : ITeamOfExplorersRepository
    {
        private readonly DataContext _dataContext;

        public TeamOfExplorersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(TeamOfExplorersFilter filter,
            int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.TeamsOfExplorers
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .ToListAsync();
        }

        public async Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            await _dataContext.TeamsOfExplorers.AddAsync(inputTeamOfExplorers);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamOfExplorers(Guid id)
        {
            _dataContext.TeamsOfExplorers.Remove(
                await _dataContext.TeamsOfExplorers.FirstOrDefaultAsync(t => t.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            _dataContext.TeamsOfExplorers.Update(inputTeamOfExplorers);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}