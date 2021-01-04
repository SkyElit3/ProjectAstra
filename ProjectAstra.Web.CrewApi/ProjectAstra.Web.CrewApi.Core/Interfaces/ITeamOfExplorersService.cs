#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface ITeamOfExplorersService
    {
        public Task<List<TeamOfExplorers>> GetAllTeamsOfExplorers(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0);

        public Task<bool> CreateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers);

        public Task<bool> DeleteTeamOfExplorers(string toSearch, List<Guid> guids);

        public Task<bool> UpdateTeamOfExplorers(TeamOfExplorers inputTeamOfExplorers);
    }
}