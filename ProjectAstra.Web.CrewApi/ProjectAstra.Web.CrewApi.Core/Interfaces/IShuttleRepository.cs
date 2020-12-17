﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IShuttleRepository
    {
        public Task<List<Shuttle>> GetAllShuttles();

        public Task<bool> CreateShuttle(Shuttle inputShuttle);

        public Task<bool> DeleteShuttle(Guid id);

        public Task<Shuttle> UpdateShuttle(Shuttle inputShuttle);
    }
}