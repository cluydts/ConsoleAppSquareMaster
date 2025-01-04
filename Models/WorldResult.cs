using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Models
{
    public class WorldResult
    {
        [BsonIgnore]
        public string WorldId { get; set; } // Verwijzing naar WorldModel.Id
        public List<EmpireResult> Empires { get; set; } = new();
    }

    public class EmpireResult
    {
        public int EmpireId { get; set; }
        public string Strategy { get; set; }
        public int ConqueredCells { get; set; }
        public double PercentageOfWorld { get; set; }
    }
}
