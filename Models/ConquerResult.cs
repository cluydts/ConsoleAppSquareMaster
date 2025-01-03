using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class ConquerResult
    {
        public string WorldId { get; set; }
        public Dictionary<int, string> EmpireStrategies { get; set; } // EmpireId -> Strategy
        public Dictionary<int, int> EmpireSizes { get; set; } // EmpireId -> Size
        public Dictionary<int, double> EmpirePercentages { get; set; } // EmpireId -> Percentage

    }
}
