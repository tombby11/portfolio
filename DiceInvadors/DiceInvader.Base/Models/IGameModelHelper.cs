using System;
using System.Collections.Generic;
using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public interface IGameModelHelper
    {
        bool CanFireBomb(int count, int wave);

        Point GetRandomBombLocation(List<Invader> invaders);
        bool CanInvadersMove(int wave, int invadersCount, DateTime lastUpdated);
        Direction GetDirectionToMoveInvaders(Direction lastInvaderDirection, double playAreaWidth, List<Invader> invaders);
    }
}