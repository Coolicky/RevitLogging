using System.Collections.Generic;

namespace Coolicky.Revit.Logging
{
    public class Activity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Details { get; set; }
    }
}