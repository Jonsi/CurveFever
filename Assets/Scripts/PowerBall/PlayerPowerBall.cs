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
    
    public abstract class PlayerPowerBall : PowerBallBase
    {
        [SerializeField] protected TargetPlayers targetPlayers;
        protected readonly Dictionary<IPlayer,PlayerStateData>  playerToStateData = new Dictionary<IPlayer, PlayerStateData>();
        protected override void Awake()
        {
            base.Awake();
            SetColor();
        }

        protected override void ApplyPower()
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

        protected override void UnApplyPower()
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

        protected virtual void ApplyOnPlayer(IPlayer player)
        {
            playerToStateData.Add(player,player.GetStateData());
        }
        protected abstract void UnApplyOnPlayer(IPlayer player);
        private void SetColor()
        {
            spriteRenderer.color = targetPlayers switch
            {
                TargetPlayers.Self => Color.blue,
                TargetPlayers.Others => Color.red,
                TargetPlayers.Everyone => Color.magenta,
                _ => spriteRenderer.color
            };
        }
    }
}