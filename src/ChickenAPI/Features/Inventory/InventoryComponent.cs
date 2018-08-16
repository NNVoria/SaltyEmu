﻿using ChickenAPI.Core.ECS.Components;
using ChickenAPI.Core.ECS.Entities;
using ChickenAPI.Enums.Game.Items;
using ChickenAPI.Game.Data.TransferObjects.Item;

namespace ChickenAPI.Game.Features.Inventory
{
    public class InventoryComponent : IComponent
    {
        private const byte WEAR_SIZE = 16;
        private const byte EQUIPMENT_SIZE = 48;
        private const byte MAIN_SIZE = 48;
        private const byte ETC_SIZE = 48;

        public InventoryComponent(IEntity entity)
        {
            Wear = new ItemInstanceDto[WEAR_SIZE];
            Equipment = new ItemInstanceDto[EQUIPMENT_SIZE];
            Main = new ItemInstanceDto[MAIN_SIZE];
            Etc = new ItemInstanceDto[ETC_SIZE];
            Entity = entity;
        }

        public ItemInstanceDto[] Equipment { get; set; }

        public ItemInstanceDto[] Wear { get; set; }

        public ItemInstanceDto[] Main { get; set; }

        public ItemInstanceDto[] Etc { get; set; }

        public ItemInstanceDto[] Specialists { get; set; }

        public ItemInstanceDto[] Costumes { get; set; }

        public IEntity Entity { get; }
    }
}