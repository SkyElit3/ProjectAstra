using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Validators
{
    public class ShuttleValidator : IShuttleValidator
    {
        // TODO: throw validation exceptions
        private Shuttle _shuttle;

        public ShuttleValidator(Shuttle inputShuttle)
        {
            _shuttle = inputShuttle;
        }

        public IShuttleValidator ValidateMaxCrewCapacity()
        {
            if (_shuttle.MaxCrewCapacity <= 0)
                throw new ValidationException("Shuttle maxCrewCapacity cannot be 0 or less.");
            if (_shuttle.TeamOfExplorers is null) return this;
            if (_shuttle.TeamOfExplorers.Explorers is null) return this;
            if (_shuttle.TeamOfExplorers.Explorers.Count > _shuttle.MaxCrewCapacity)
                throw new ValidationException(
                    "Shuttle's Team of Explorers crew size exceeds the Shuttle maxCrewCapacity.");
            return this;
        }

        public IShuttleValidator ValidateName()
        {
            if (_shuttle.Name.Length <= 1)
                throw new ValidationException("Shuttle's Name cannot be 1 character or less.");
            return this;
        }
    }
}