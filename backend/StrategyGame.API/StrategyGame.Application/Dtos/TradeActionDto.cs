using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Application.Dtos
{
    public class TradeActionDto
    {
        public Guid TradeId { get; set; }
        public int Amount { get; set; }
    }
}
