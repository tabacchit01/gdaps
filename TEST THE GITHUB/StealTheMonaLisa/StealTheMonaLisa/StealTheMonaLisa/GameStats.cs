using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StealTheMonaLisa
{
    class GameStats
    {
        private int money;
        private int infamy;
        private int level;
        private double lvlexp;
        private double infexp;

        public GameStats(int mny, int infmy, int lvl, double lxp, double ixp)
        {
            money = mny;
            infamy = infmy;
            level = lvl;
            lvlexp = lxp;
            infexp = ixp;
        }
    }
}
