﻿using System.Threading.Tasks;
using ChickenAPI.Data.Skills;
using ChickenAPI.Enums.Game.Entity;
using ChickenAPI.Enums.Packets;
using ChickenAPI.Game.Battle.Extensions;
using ChickenAPI.Game.Battle.Interfaces;
using ChickenAPI.Game.Entities.Monster;
using ChickenAPI.Game.Entities.Npc;
using ChickenAPI.Game.Entities.Player;
using ChickenAPI.Game.Skills.Args;
using ChickenAPI.Packets.Game.Client.Battle;
using ChickenAPI.Packets.Game.Server.Battle;
using NW.Plugins.PacketHandling.Utils;

namespace NW.Plugins.PacketHandling.Skill
{
    public class UseSkillPacketHandling : GenericGamePacketHandlerAsync<UseSkillPacket>
    {
        protected override async Task Handle(UseSkillPacket packet, IPlayerEntity player)
        {
            if (packet.CastId != 0)
            {
                await player.SendPacketAsync(new MscPacket());
            }

            if (player.IsSitting)
            {
                player.IsSitting = false;
            }

            if (!player.SkillComponent.SkillsByCastId.TryGetValue(packet.CastId, out SkillDto skill))
            {
                // skill does not exist
                await player.SendPacketAsync(player.GenerateEmptyCancelPacket(CancelPacketType.NotInCombatMode));
                Log.Warn($"{player.Character.Name} Trying to cast unowned skill");
                return;
            }

            IBattleEntity target = null;
            switch (packet.TargetVisualType)
            {
                case VisualType.Character:
                    target = player.CurrentMap.GetEntity<IPlayerEntity>(packet.TargetId, VisualType.Character);
                    break;
                case VisualType.Monster:
                    target = player.CurrentMap.GetEntity<IMonsterEntity>(packet.TargetId, VisualType.Monster);
                    break;
                case VisualType.Npc:
                    target = player.CurrentMap.GetEntity<INpcEntity>(packet.TargetId, VisualType.Npc);
                    break;
            }

            switch (target)
            {
                case null:
                    await player.SendPacketAsync(player.GenerateEmptyCancelPacket(CancelPacketType.InCombatMode));
                    return;
                case IBattleEntity battleEntity:

                    await player.EmitEventAsync(new UseSkillEvent
                    {
                        Skill = skill,
                        Target = battleEntity
                    });
                    break;
            }
        }
    }
}