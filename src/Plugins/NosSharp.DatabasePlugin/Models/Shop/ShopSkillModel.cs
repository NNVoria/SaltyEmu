﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChickenAPI.Core.Data.TransferObjects;
using NosSharp.DatabasePlugin.Models.Skill;

namespace NosSharp.DatabasePlugin.Models.Shop
{
    [Table("shop_skill")]
    public class ShopSkillModel : IMappedDto
    {
        [Key]
        public long Id { get; set; }

        public SkillModel Skill { get; set; }

        [ForeignKey(nameof(SkillId))]
        public long SkillId { get; set; }

        public ShopModel Shop { get; set; }

        [ForeignKey(nameof(ShopId))]
        public long ShopId { get; set; }

        public byte Slot { get; set; }

        public byte Type { get; set; }
    }
}