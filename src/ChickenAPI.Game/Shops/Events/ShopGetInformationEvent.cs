﻿using ChickenAPI.Game.Events;

namespace ChickenAPI.Game.Shops.Events
{
    public class ShopGetInformationEvent : ChickenEventArgs
    {
        public Shop Shop { get; set; }

        public byte Type { get; set; }
    }
}