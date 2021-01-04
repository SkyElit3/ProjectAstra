using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IHumanCaptainRepository
    {
        public Task<List<HumanCaptain>> GetAllHumanCaptains(HumanCaptainFilter filter, int pagination = 50, int skip = 0);

        public Task<bool> CreateHumanCaptain(HumanCaptain inputHumanCaptain);

        public Task<bool> DeleteHumanCaptain(Guid id);

        public Task<bool> UpdateHumanCaptain(HumanCaptain inputHumanCaptain);
    }
}