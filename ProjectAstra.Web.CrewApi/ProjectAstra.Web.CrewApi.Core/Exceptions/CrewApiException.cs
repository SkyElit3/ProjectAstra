using System;
using Microsoft.Extensions.Logging;
using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Exceptions
{
    public class CrewApiException : Exception
    {
        public ExceptionType Type { get; set; }

        public string Message { get; set; }

        private Exception RootException { get; set; }

        public ExceptionSeverity Severity { get; set; }

        public void LogException(ILogger logger)
        {
            switch (Severity)
            {
                //if root Exception is null don t put it as a parameter ~ how ??? 
                case ExceptionSeverity.Warning:
                    if (RootException == null)
                    {
                        logger.LogWarning($"{Type} : {Message}");
                        break;
                    }

                    logger.LogWarning(RootException, $"{Type} : {Message}");
                    break;

                case ExceptionSeverity.Error:
                    if (RootException == null)
                    {
                        logger.LogError($"{Type} : {Message}");
                        break;
                    }

                    logger.LogError(RootException, $"{Type} : {Message}");
                    break;

                case ExceptionSeverity.Critical:
                    if (RootException == null)
                    {
                        logger.LogCritical($"{Type} : {Message}");
                        break;
                    }

                    logger.LogCritical(RootException, $"{Type} : {Message}");
                    break;

                default:
                    logger.LogCritical($"{Type} : {Message}");
                    break;
            }
        }
    }
}