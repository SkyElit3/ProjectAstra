#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IHumanCaptainService
    {
        public Task<List<HumanCaptain>> GetAllHumanCaptains(string toSearch, List<Guid> guids, int pagination = 50, int skip = 0);

        public Task<bool> CreateHumanCaptain(HumanCaptain inputHumanCaptain);

        public Task<bool> DeleteHumanCaptain(string toSearch, List<Guid> guids);

        public Task<bool> UpdateHumanCaptain(HumanCaptain inputHumanCaptain);
    }
}