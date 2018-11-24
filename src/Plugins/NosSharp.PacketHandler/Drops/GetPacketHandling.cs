﻿using System.Linq;
using ChickenAPI.Core.Utils;
using ChickenAPI.Enums.Game.Entity;
using ChickenAPI.Game.Entities.Drop;
using ChickenAPI.Game.Entities.Player;
using ChickenAPI.Game.Entities.Player.Extensions;
using ChickenAPI.Game.Player.Extension;
using ChickenAPI.Packets.Game.Client.Drops;

namespace NosSharp.PacketHandler.Drops
{
    public class GetPacketHandling
    {
        public static void OnGetPacket(GetPacket packet, IPlayerEntity player)
        {
            var mapItem = player.CurrentMap.GetEntitiesByType<IDropEntity>(VisualType.MapObject)
                .FirstOrDefault(i => i.Id == packet.DropId);

            if (mapItem == null || PositionHelper.GetDistance(mapItem.Position, player.Actual) > 8)
            {
                return;
            }

            if (mapItem.ItemVnum == 1046) // Gold
            {
                player.GoldUp(mapItem.Quantity);
                player.CurrentMap.Broadcast(player.GenerateGetPacket(mapItem.Id));
                // Should remove the entity
            }
        }
    }
}