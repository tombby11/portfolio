using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceInvader.Base.Models
{
    public class GameModelHelper
    {
        private readonly Random _random;

        public GameModelHelper()
        {
            _random = new Random();
        }

        public  bool CanFireBomb(int count, int wave)
        {
            if (count > wave + 1)
                return false;

            if (_random.Next(10) < 10 - wave)
                return false;

            return true;
        }
    }
}
