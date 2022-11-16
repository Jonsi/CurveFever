using System;
using System.Collections.Generic;
using Managers;
using Player;
using UnityEngine;

namespace PowerBall
{
    public enum TargetPlayers
    {
        Self,
        Others,
        Everyone
    }
    
    public abstract class PlayerPowerBall : PowerBall
    {
        public TargetPlayers targetPlayers;
        protected readonly Dictionary<IPlayer,PlayerStateData>  playerToStateData = new Dictionary<IPlayer, PlayerStateData>();
        
        public override void ApplyPower(IPlayer hitPlayer)
        {
            switch (targetPlayers)
            {
                case TargetPlayers.Self:
                    ApplyOnPlayer(hitPlayer);
                    break;
                
                case TargetPlayers.Others:
                    var otherPlayers = PlayerManager.Instance.GetAlivePlayers();
                    otherPlayers.Remove(hitPlayer);
                    foreach (var player in otherPlayers)
                    {
                        ApplyOnPlayer(player);
                    }
                    break;
                case TargetPlayers.Everyone:
                    var allPlayers = PlayerManager.Instance.GetAlivePlayers();
                    foreach (var player in allPlayers)
                    {
                        ApplyOnPlayer(player);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void UnApplyPower(IPlayer hitPlayer)
        {
            switch (targetPlayers)
            {
                case TargetPlayers.Self:
                    UnApplyOnPlayer(hitPlayer);
                    break;
                
                case TargetPlayers.Others:
                    var otherPlayers = PlayerManager.Instance.GetAlivePlayers();
                    otherPlayers.Remove(hitPlayer);
                    foreach (var player in otherPlayers)
                    {
                        UnApplyOnPlayer(player);
                    }
                    break;
                case TargetPlayers.Everyone:
                    var allPlayers = PlayerManager.Instance.GetAlivePlayers();
                    foreach (var player in allPlayers)
                    {
                        UnApplyOnPlayer(player);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override Color GetColor()
        {
            var color = targetPlayers switch
            {
                TargetPlayers.Self => Color.blue,
                TargetPlayers.Others => Color.red,
                TargetPlayers.Everyone => Color.magenta,
                _ => Color.white
            };
            return color;
        }

        protected virtual void ApplyOnPlayer(IPlayer player)
        {
            playerToStateData.Add(player,player.GetStateData());
        }

        protected abstract void UnApplyOnPlayer(IPlayer player);

    }
}