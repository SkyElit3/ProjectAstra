using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Interfaces
{
    public interface IShuttleValidator
    {
        public IShuttleValidator ValidateMaxCrewCapacity();

        public IShuttleValidator ValidateName();
    }
}