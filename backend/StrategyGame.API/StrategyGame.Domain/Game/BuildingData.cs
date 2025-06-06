﻿using StrategyGame.Common.Enums;
using StrategyGame.Common.Stores;
using System;
using System.Collections.Generic;

namespace StrategyGame.Domain.Game
{
    public class BuildingData : IEntity
    {
        public Guid Id { get; set; }

        public BuildingType Type { get; set; }

        public int Value { get; set; }

        public Guid? FactoryParametersId { get; set; }
        public FactoryParameter? FactoryParameters { get; set; }
        
        public IEnumerable<BuildingPrice> Cost { get; set; } = default!;
    }
}
