using System.Threading.Tasks;
using ProjectAstra.Web.CrewApi.Core.Filters;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IExplorerRepository
    {
        public Task<int> GetAllExplorers(ExplorerFilter filter, int pagination = 50, int skip = 0);
    }
}