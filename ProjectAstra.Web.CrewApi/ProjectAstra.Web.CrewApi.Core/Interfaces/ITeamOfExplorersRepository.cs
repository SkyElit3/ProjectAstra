using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface ITeamOfExplorersRepository
    {
        public Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(TeamOfExplorersFilter filter, int pagination = 50, int skip = 0);

        public Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeam);

        public Task<bool> DeleteTeamOfExplorers(Guid id);

        public Task<TeamOfExplorers> UpdateTeamOfExplorers(TeamOfExplorers inputTeam);
    }
}