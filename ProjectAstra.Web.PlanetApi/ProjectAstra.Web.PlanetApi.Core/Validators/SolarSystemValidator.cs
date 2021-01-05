using ProjectAstra.Web.PlanetApi.Core.Enums;
using ProjectAstra.Web.PlanetApi.Core.Exceptions;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Models;

namespace ProjectAstra.Web.PlanetApi.Core.Validators
{
    public class SolarSystemValidator : ISolarSystemValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((SolarSystem) entity) && ValidateOrbitalSpeed((SolarSystem) entity);
        }
        
        private static bool ValidateName(SolarSystem inputSolarSystem)
        {
            if (inputSolarSystem.Name.Length <= 1)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System's Name {inputSolarSystem.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }

        private static bool ValidateOrbitalSpeed(SolarSystem inputSolarSystem)
        {
            if(inputSolarSystem.OrbitalSpeed <= 0)
                throw new PlanetApiException
                {
                    ExceptionMessage = $"Solar System's orbital speed {inputSolarSystem.OrbitalSpeed} cannot be 0 or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}