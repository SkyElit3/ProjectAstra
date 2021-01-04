using System.Text.RegularExpressions;
using ProjectAstra.Web.CrewApi.Core.Enums;
using ProjectAstra.Web.CrewApi.Core.Exceptions;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Models;

namespace ProjectAstra.Web.CrewApi.Core.Validators
{
    public class HumanCaptainValidator : IHumanCaptainValidator
    {
        public bool Validate(IEntity entity)
        {
            return ValidateName((HumanCaptain) entity) && ValidateGrade((HumanCaptain) entity) &&
                   ValidatePassword((HumanCaptain) entity);
        }

        private static bool ValidateName(HumanCaptain inputHumanCaptain)
        {
            if (inputHumanCaptain.Name.Length <= 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Name {inputHumanCaptain.Name} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }

        private static bool ValidateGrade(HumanCaptain inputHumanCaptain)
        {
            if (inputHumanCaptain.Grade.Length <= 1)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Grade {inputHumanCaptain.Grade} cannot be 1 character or less.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }

        private static bool ValidatePassword(HumanCaptain inputHumanCaptain)
        {
            if (inputHumanCaptain.Password.Length < 6)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Password {inputHumanCaptain.Password} cannot be less than 6 characters.",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            if (!Regex.Match(inputHumanCaptain.Password, @"[a-z]", RegexOptions.ECMAScript).Success)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Password {inputHumanCaptain.Password} must contain at least one lowercase character from a-z .",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            if (!Regex.Match(inputHumanCaptain.Password, @"[A-Z]", RegexOptions.ECMAScript).Success)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Password {inputHumanCaptain.Password} must contain at least one uppercase character from a-z .",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            if (!Regex.Match(inputHumanCaptain.Password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript)
                .Success)
                throw new CrewApiException
                {
                    ExceptionMessage = $"HumanCaptain's Password {inputHumanCaptain.Password} must contain at least one symbol .",
                    Severity = ExceptionSeverity.Error,
                    Type = ExceptionType.ValidationException
                };
            return true;
        }
    }
}