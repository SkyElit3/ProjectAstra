using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Filters;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IExplorerRepository
    {
        public Task<int> GetAllExplorers(ExplorerFilter filter, int pagination = 50, int skip = 0);
    }
}