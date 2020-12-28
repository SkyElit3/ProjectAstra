using ProjectAstra.Web.CrewApi.Core.Enums;

namespace ProjectAstra.Web.CrewApi.Core.Models
{
    public class Robot : Explorer
    {
        public RobotType Type { get; set; }

        public int UnitsOfEnergy { get; set; }
    }
}