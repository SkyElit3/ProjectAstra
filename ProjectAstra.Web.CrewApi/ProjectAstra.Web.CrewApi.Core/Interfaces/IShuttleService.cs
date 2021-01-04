#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IShuttleService
    {
        public Task<List<Shuttle>> GetAllShuttles(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0);

        public Task<bool> CreateShuttle(Shuttle inputShuttle);

        public Task<bool> DeleteShuttle(string toSearch, List<Guid> guids);

        public Task<bool> UpdateShuttle(Shuttle inputShuttle);
    }
}