﻿using System;
using System.Collections.Generic;
using ChickenAPI.Core.Utils;
using ChickenAPI.Data.Map;
using ChickenAPI.Data.NpcMonster;
using ChickenAPI.Enums.Game.Entity;
using ChickenAPI.Enums.Game.Visibility;
using ChickenAPI.Game.Battle.DataObjects;
using ChickenAPI.Game.ECS.Components;
using ChickenAPI.Game.ECS.Entities;
using ChickenAPI.Game.Features.Skills;
using ChickenAPI.Game.Movements.DataObjects;
using ChickenAPI.Game.Visibility;

namespace ChickenAPI.Game.Entities.Monster
{
    public class MonsterEntity : EntityBase, IMonsterEntity
    {
        public MonsterEntity(MapMonsterDto dto) : base(VisualType.Monster, dto.Id)
        {
            Movable = new MovableComponent(this, dto.IsMoving ? dto.NpcMonster.Speed : (byte)0)
            {
                Actual = new Position<short> { X = dto.MapX, Y = dto.MapY },
                Destination = new Position<short> { X = dto.MapX, Y = dto.MapY }
            };
            Battle = new BattleComponent(this)
            {
                Hp = dto.NpcMonster.MaxHp,
                HpMax = dto.NpcMonster.MaxHp,
                Mp = dto.NpcMonster.MaxMp,
                MpMax = dto.NpcMonster.MaxMp,
                BasicArea = dto.NpcMonster.BasicArea
            };
            Skills = new SkillComponent(this);
            NpcMonster = dto.NpcMonster;
            MapMonster = dto;
            _visibility = new VisibilityComponent(this);
            Components = new Dictionary<Type, IComponent>
            {
                { typeof(VisibilityComponent), _visibility },
                { typeof(BattleComponent), Battle },
                { typeof(MovableComponent), Movable },
                { typeof(NpcMonsterComponent), new NpcMonsterComponent(this, dto) },
                { typeof(SkillComponent), Skills }
            };
        }

        public SkillComponent Skills { get; }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public MovableComponent Movable { get; }
        public BattleComponent Battle { get; set; }
        public NpcMonsterDto NpcMonster { get; }
        public MapMonsterDto MapMonster { get; }

        #region Visibility

        private VisibilityComponent _visibility { get; }

        public event EventHandlerWithoutArgs<IVisibleEntity> Invisible
        {
            add => _visibility.Invisible += value;
            remove => _visibility.Invisible -= value;
        }

        public event EventHandlerWithoutArgs<IVisibleEntity> Visible
        {
            add => _visibility.Visible += value;
            remove => _visibility.Visible -= value;
        }

        public bool IsVisible => _visibility.IsVisible;

        public bool IsInvisible => _visibility.IsInvisible;

        public VisibilityType Visibility
        {
            get => _visibility.Visibility;
            set => _visibility.Visibility = value;
        }

        #endregion
    }
}