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
        public Dictionary<int, string> EmpireStrategies { get; set; }
        public Dictionary<int, int> EmpireSizes { get; set; }
        public Dictionary<int, double> EmpirePercentages { get; set; }

    }
}
