using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectAstra.Web.CrewApi.Core.Extensions;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;
using ProjectAstra.Web.CrewApi.Infrastructure.Extensions;

namespace ProjectAstra.Web.CrewApi.Infrastructure.Repositories
{
    public class HumanCaptainRepository : IHumanCaptainRepository
    {
        private readonly DataContext _dataContext;

        public HumanCaptainRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<HumanCaptain>> GetAllHumanCaptains(HumanCaptainFilter filter, int pagination = 50,
            int skip = 0)
        {
            return await filter.Filter(_dataContext.HumanCaptains
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .ToListAsync();
        }

        public async Task<bool> CreateHumanCaptain(HumanCaptain inputHumanCaptain)
        {
            await _dataContext.HumanCaptains.AddAsync(inputHumanCaptain);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteHumanCaptain(Guid id)
        {
            _dataContext.HumanCaptains.Remove(
                await _dataContext.HumanCaptains.FirstOrDefaultAsync(humanCaptain => humanCaptain.Id.Equals(id)));
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHumanCaptain(HumanCaptain inputHumanCaptain)
        {
            _dataContext.HumanCaptains.Update(inputHumanCaptain);
            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}