using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IShuttleRepo
    {
        public Task<Shuttle> GetShuttle(Guid id);

        public Task<List<Shuttle>> GetAllShuttles();

        public Task<Shuttle> CreateShuttle(Shuttle inputShuttle);

        public Task<Shuttle> DeleteShuttle(Guid id);

        public Task<Shuttle> UpdateShuttle(Shuttle inputShuttle);
    }
}