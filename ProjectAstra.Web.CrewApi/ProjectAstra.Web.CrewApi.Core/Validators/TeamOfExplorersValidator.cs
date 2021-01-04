using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Validators
{
    public class TeamOfExplorersValidator : ITeamOfExplorersValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((TeamOfExplorers) entity);
        }

        private static bool ValidateName(TeamOfExplorers inputTeamOfExplorers)
        {
            if (inputTeamOfExplorers.Name.Length <= 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"TeamOfExplorers's Name {inputTeamOfExplorers.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}