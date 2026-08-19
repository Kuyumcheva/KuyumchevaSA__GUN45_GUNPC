using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Units
{
    public abstract class UnitFactory
    {
        public abstract Unit CreatePlayer(string name);
        public abstract Unit CreateGoblinEnemy();
    }
}
