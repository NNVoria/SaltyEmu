﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChickenAPI.Data.Item;
using ChickenAPI.Enums;
using ChickenAPI.Enums.Game.Items;
using Microsoft.EntityFrameworkCore;
using SaltyEmu.Database;
using SaltyEmu.DatabasePlugin.Models.Character;

namespace SaltyEmu.DatabasePlugin.Services.Character
{
    public class CharacterItemDao : SynchronizedRepositoryBase<ItemInstanceDto, CharacterItemModel>, IItemInstanceService
    {
        public CharacterItemDao(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<(long, ItemDto)> GetBaseInventory(AuthorityType authorityType) => null;

        public IEnumerable<ItemInstanceDto> GetWearByCharacterId(long id)
        {
            try
            {
                return DbSet.Where(s => s.CharacterId == id).AsEnumerable().Select(Mapper.Map<ItemInstanceDto>).ToArray();
            }
            catch (Exception e)
            {
                Log.Error("[GET_WEAR_BY_CHARACTER_ID]", e);
                return null;
            }
        }

        public async Task<IEnumerable<ItemInstanceDto>> GetWearByCharacterIdAsync(long characterId)
        {
            try
            {
                return (await DbSet.Where(s => s.CharacterId == characterId && s.Item.Type == InventoryType.Wear).ToArrayAsync()).Select(Mapper.Map<ItemInstanceDto>).ToArray();
            }
            catch (Exception e)
            {
                Log.Error("[GET_WEAR_BY_CHARACTER_ID]", e);
                return null;
            }
        }

        public IEnumerable<ItemInstanceDto> GetByCharacterId(long characterId)
        {
            try
            {
                return DbSet.Where(s => s.CharacterId == characterId).ToArray().Select(Mapper.Map<ItemInstanceDto>).ToArray();
            }
            catch (Exception e)
            {
                Log.Error("[GET_BY_CHARACTER_ID]", e);
                return null;
            }
        }

        public async Task<IEnumerable<ItemInstanceDto>> GetByCharacterIdAsync(long characterId)
        {
            try
            {
                return (await DbSet.Where(s => s.CharacterId == characterId).ToArrayAsync()).Select(Mapper.Map<ItemInstanceDto>).ToArray();
            }
            catch (Exception e)
            {
                Log.Error("[GET_BY_CHARACTER_ID]", e);
                return null;
            }
        }
    }
}