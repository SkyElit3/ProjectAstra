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
            return ValidateName((TeamOfExplorers) entity) && ValidateStatus((TeamOfExplorers) entity);
        }
        
        private static bool ValidateStatus(TeamOfExplorers inputTeamOfExplorers)
        {
            // ??? error occurs in json when object is created in controller if status is not of statustype
            // should i bother validating it then ?
            return true;
        }

        private static bool ValidateName(TeamOfExplorers inputTeamOfExplorers)
        {
            if (inputTeamOfExplorers.Name.Length <= 1)
                throw new CrewApiException
                {
                    Message = "TeamOfExplorers's Name cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}