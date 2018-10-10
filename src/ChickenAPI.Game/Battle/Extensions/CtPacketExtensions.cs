﻿using ChickenAPI.Data.Skills;
using ChickenAPI.Enums.Game.Entity;
using ChickenAPI.Game.ECS.Entities;
using ChickenAPI.Game.Entities.Monster;
using ChickenAPI.Game.Entities.Npc;
using ChickenAPI.Game.Entities.Player;
using ChickenAPI.Packets.Game.Server.Battle;

namespace ChickenAPI.Game.Battle.Extensions
{
    public static class CtPacketExtensions
    {
        public static CtPacket GenerateCtPacket(IEntity entity, IEntity target, SkillDto skill)
        {
            var ct = new CtPacket
            {
                CastAnimationId = skill.CastAnimation,
                CastEffect = skill.CastEffect,
                SkillId = skill.Id,
            };
            switch (entity)
            {
                case IPlayerEntity player:
                    ct.VisualType = VisualType.Character;
                    ct.VisualId = player.Character.Id;
                    break;

                case INpcEntity npc:
                    ct.VisualType = VisualType.Npc;
                    ct.VisualId = npc.MapNpc.Id;
                    break;

                case IMonsterEntity monster:
                    ct.VisualType = VisualType.Monster;
                    ct.VisualId = monster.MapMonster.Id;
                    break;
            }
            switch (target)
            {
                case IPlayerEntity player:
                    ct.TargetVisualType = VisualType.Character;
                    ct.TargetId = player.Character.Id;
                    break;

                case INpcEntity npc:
                    ct.TargetVisualType = VisualType.Npc;
                    ct.TargetId = npc.MapNpc.Id;
                    break;

                case IMonsterEntity monster:
                    ct.TargetVisualType = VisualType.Monster;
                    ct.TargetId = monster.MapMonster.Id;
                    break;
            }

            return ct;
        }
    }
}