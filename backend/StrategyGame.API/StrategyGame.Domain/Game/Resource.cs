﻿using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using StrategyGame.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Domain.Game
{
    public class Resource : IEntity
    {
        public Guid Id { get; set; }

        public Guid StrategyGameUserId { get; set; }
        public StrategyGameUser StrategyGameUser { get; set; }

        public int Amount { get; set; }

        public Guid ResourceDataId { get; set; }

        public ResourceData ResourceData { get; set; }
    }
}
