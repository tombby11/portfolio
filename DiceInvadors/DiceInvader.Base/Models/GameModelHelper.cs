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

        public  virtual bool CanFireBomb(int count, int wave) //having it as a virtual so it can be supported by MOQ   // TODO : do something about it 
        {
            if (count > wave + 1)
                return false;

            if (_random.Next(10) < 10 - wave)
                return false;

            return true;
        }
    }
}
