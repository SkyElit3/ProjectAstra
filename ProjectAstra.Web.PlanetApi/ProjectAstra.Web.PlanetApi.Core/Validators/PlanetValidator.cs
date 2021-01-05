using ProjectAstra.Web.PlanetApi.Core.Enums;
using ProjectAstra.Web.PlanetApi.Core.Exceptions;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Validators
{
    public class PlanetValidator : IPlanetValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((Planet) entity) && ValidateOrbitalSpeed((Planet) entity);
        }
        
        private static bool ValidateName(Planet inputPlanet)
        {
            if (inputPlanet.Name.Length <= 1)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet's Name {inputPlanet.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }

        private static bool ValidateOrbitalSpeed(Planet inputPlanet)
        {
            if(inputPlanet.OrbitalSpeed <= 0)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet's orbital speed {inputPlanet.OrbitalSpeed} cannot be 0 or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
        
        private static bool ValidateImage(Planet inputPlanet)
        {
            if(inputPlanet.Image.Length <= 1)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet's image {inputPlanet.OrbitalSpeed} inexistent.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
        
        private static bool ValidateNumberOfRobots(Planet inputPlanet)
        {
            if(inputPlanet.NumberOfRobots <= 0)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Planet's number of robots {inputPlanet.OrbitalSpeed} cannot be 0 or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}