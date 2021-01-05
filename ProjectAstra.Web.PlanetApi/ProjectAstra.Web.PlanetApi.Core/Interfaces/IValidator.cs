using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Interfaces
{
    public interface IValidator
    {
        public bool Validate(IEntity entity);
    }
}