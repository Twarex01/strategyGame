using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Dtos
{
    public class BattleDoneDto
    {
        public bool Success { get; set; }

        public int UnitsLost { get; set; }
    }
}
