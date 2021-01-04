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
            return ValidateName((Shuttle) entity);
        }

        private static bool ValidateName(Shuttle inputShuttle)
        {
            if (inputShuttle.Name.Length <= 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"Shuttle's Name {inputShuttle.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}