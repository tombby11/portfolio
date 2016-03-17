using System;
using System.Collections.Generic;
using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public interface IGameModelHelper
    {
        bool CanFireBomb(int count, int wave);

        Point GetRandomBombLocation(List<Invader> invaders);
        bool CanInvadorsMove(int wave, int invadersCount, DateTime lastUpdated);
    }
}