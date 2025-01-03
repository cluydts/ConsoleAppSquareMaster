using ConsoleAppSquareMaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class Empire
    {
        public int Id { get; set; }
        public IConquerStrategy Strategy { get; set; }

        public Empire(int id, IConquerStrategy strategy)
        {
            Id = id;
            Strategy = strategy;
        }

        public void ExecuteConquer(WorldConquer conquer, int turns)
        {
            Strategy.Conquer(conquer, Id, turns);
        }
    }
}
