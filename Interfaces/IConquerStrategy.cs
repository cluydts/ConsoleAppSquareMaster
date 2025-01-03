using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Interfaces
{
    public interface IConquerStrategy
    {
        void Conquer(WorldConquer conquer, int empireId, int turns);
    }
}
