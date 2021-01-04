using System.Text.RegularExpressions;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Validators
{
    public class RobotValidator : IRobotValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((Robot) entity) &&
                   ValidateUnitsOfEnergy((Robot) entity);
        }

        private static bool ValidateName(Robot inputRobot)
        {
            if (inputRobot.Name.Length <= 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"Robot's Name {inputRobot.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }

        private static bool ValidateUnitsOfEnergy(Robot inputRobot)
        {
            if(inputRobot.UnitsOfEnergy <= 0)
                throw new CrewApiException
                {
                    ExceptionMessage = $"Robot's Units of energy {inputRobot.UnitsOfEnergy} cannot be 0 or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}