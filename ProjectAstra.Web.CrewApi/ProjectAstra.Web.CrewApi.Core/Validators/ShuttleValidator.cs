using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Validators
{
    public class ShuttleValidator : IShuttleValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((Shuttle) entity) && ValidateMaxCrewCapacity((Shuttle) entity);
        }

        private static bool ValidateMaxCrewCapacity(Shuttle inputShuttle)
        {
            if (inputShuttle.MaxCrewCapacity <= 0)
                throw new CrewApiException("Shuttle maxCrewCapacity cannot be 0 or less.",
                    ExceptionTypeEnum.ValidationException);
            if (inputShuttle.TeamOfExplorers?.Explorers is null) return true;
            if (inputShuttle.TeamOfExplorers.Explorers.Count > inputShuttle.MaxCrewCapacity)
                throw new CrewApiException(
                    "Shuttle's Team of Explorers crew size exceeds the Shuttle maxCrewCapacity.",
                    ExceptionTypeEnum.ValidationException);
            return true;
        }

        private static bool ValidateName(Shuttle inputShuttle)
        {
            if (inputShuttle.Name.Length <= 1)
                throw new CrewApiException("Shuttle's Name cannot be 1 character or less.",
                    ExceptionTypeEnum.ValidationException);
            return true;
        }
    }
}