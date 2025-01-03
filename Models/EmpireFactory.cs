using ConsoleAppSquareMaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class EmpireFactory
    {
        public static Empire CreateEmpire(int id, string strategyType)
        {
            IConquerStrategy strategy = strategyType switch
            {
                "Conquer1" => new Conquer1Strategy(),
                "Conquer2" => new Conquer2Strategy(),
                "Conquer3" => new Conquer3Strategy(),
                _ => throw new ArgumentException("Invalid strategy type")
            };

            return new Empire(id, strategy);
        }
    }
}
