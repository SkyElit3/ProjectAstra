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
    public class ExplorerRepository : IExplorerRepository
    {
        private readonly DataContext _dataContext;

        public ExplorerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> GetAllExplorers(ExplorerFilter filter, int pagination = 50, int skip = 0)
        {
            return await filter.Filter(_dataContext.HumanCaptains
                    .AsQueryable())
                .Skip(skip)
                .Take(pagination)
                .CountAsync();
        }
    }
}