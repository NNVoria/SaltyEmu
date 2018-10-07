﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ChickenAPI.Data;
using ChickenAPI.Data.Map;

namespace ChickenAPI.Game.Data.AccessLayer.Map
{
    public interface IMapMonsterService : IMappedRepository<MapMonsterDto>
    {
        IEnumerable<MapMonsterDto> GetByMapId(long mapId);
        Task<IEnumerable<MapMonsterDto>> GetByMapIdAsync(long mapId);
    }
}