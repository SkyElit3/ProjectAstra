/*using System;
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
    public class TeamOfExplorersRepository : ITeamOfExplorersRepository
    {
        private readonly DataContext _dataContext;

        public TeamOfExplorersRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(TeamOfExplorersFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.TeamOfExplorerss.AsQueryable()).Skip(skip).Take(pagination).ToListAsync();
        }

        public async Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            await _dataContext.TeamOfExplorerss.AddAsync(inputTeamOfExplorers);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamOfExplorers(Guid id)
        {
            _dataContext.TeamOfExplorerss.Remove(
                await _dataContext.TeamOfExplorerss.FirstOrDefaultAsync(TeamOfExplorers => TeamOfExplorers.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<TeamOfExplorers> UpdateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers)
        {
            var TeamOfExplorersToUpdate =
                (await _dataContext.TeamOfExplorerss.FirstOrDefaultAsync(TeamOfExplorers => TeamOfExplorers.Id.Equals(inputTeamOfExplorers.Id)));
            TeamOfExplorersToUpdate.UpdateByReflection(inputTeamOfExplorers);
            await _dataContext.SaveChangesAsync();
            return TeamOfExplorersToUpdate;
        }
    }
}*/