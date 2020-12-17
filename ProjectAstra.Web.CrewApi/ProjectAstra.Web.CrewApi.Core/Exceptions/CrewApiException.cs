using System;
using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Exceptions
{
    public class CrewApiException : Exception
    {
        public ExceptionTypeEnum TypeEnum { get; }

        public CrewApiException(ExceptionTypeEnum typeEnum)
        {
            TypeEnum = typeEnum;
        }

        public CrewApiException(string message, ExceptionTypeEnum typeEnum) : base(message)
        {
            TypeEnum = typeEnum;
        }

        public CrewApiException(string message, Exception inner, ExceptionTypeEnum typeEnum) : base(message, inner)
        {
            TypeEnum = typeEnum;
        }
    }
}