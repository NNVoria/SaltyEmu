﻿using System;
using System.Collections.Generic;
using ChickenAPI.Game.Battle.Events;
using ChickenAPI.Game.Battle.Extensions;
using ChickenAPI.Game.Battle.Hitting;
using ChickenAPI.Game.Battle.Interfaces;
using ChickenAPI.Game.ECS.Entities;
using ChickenAPI.Game.Events;

namespace ChickenAPI.Game.Battle
{
    public class BattleEventHandler : EventHandlerBase
    {
        public override ISet<Type> HandledTypes => new HashSet<Type>
        {
            typeof(ProcessHitRequestEvent),
            typeof(FillHitRequestEvent),
            typeof(TargetHitRequest)
        };

        public override void Execute(IEntity entity, ChickenEventArgs e)
        {
            if (!(entity is IBattleEntity battleEntity))
            {
                return;
            }
            switch (e)
            {
                case ProcessHitRequestEvent processHitRequest:
                    // do the whole graphical & stats processing
                    break;
                case FillHitRequestEvent fillHitRequest:
                    SetupHitType(fillHitRequest.HitRequest);
                    FillEffects(fillHitRequest.HitRequest);
                    DamageCalculation(fillHitRequest.HitRequest);
                    break;
                case TargetHitRequest targetHit:
                    BattleExtensions.TargetHit(battleEntity, targetHit);
                    break;
            }
        }

        private void DamageCalculation(HitRequest hitRequest)
        {
        }

        private void FillEffects(HitRequest hitRequest)
        {
        }

        private void SetupHitType(HitRequest hitRequest)
        {
        }
    }
}