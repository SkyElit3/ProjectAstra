using System;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Exceptions
{
    public class CrewApiException : Exception
    {
        public ExceptionType Type { get; set; }

        public string ExceptionMessage { get; set; }

        private Exception RootException { get; set; }

        public ExceptionSeverity Severity { get; set; }

        public void LogException(ILogger logger)
        {
            switch (Severity)
            {
                case ExceptionSeverity.Warning:
                    if (RootException == null)
                    {
                        logger.LogWarning($"{Type} : {ExceptionMessage}");
                        break;
                    }

                    logger.LogWarning(RootException, $"{Type} : {ExceptionMessage}");
                    break;

                case ExceptionSeverity.Error:
                    if (RootException == null)
                    {
                        logger.LogError($"{Type} : {ExceptionMessage}");
                        break;
                    }

                    logger.LogError(RootException, $"{Type} : {ExceptionMessage}");
                    break;

                case ExceptionSeverity.Critical:
                    if (RootException == null)
                    {
                        logger.LogCritical($"{Type} : {ExceptionMessage}");
                        break;
                    }

                    logger.LogCritical(RootException, $"{Type} : {ExceptionMessage}");
                    break;

                default:
                    logger.LogCritical($"{Type} : {ExceptionMessage}");
                    break;
            }
        }
    }
}