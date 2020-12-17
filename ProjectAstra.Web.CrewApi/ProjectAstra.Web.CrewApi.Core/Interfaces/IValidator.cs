using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IValidator
    {
        public bool Validate(IEntity entity);
    }
}