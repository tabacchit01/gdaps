using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StealTheMonaLisa
{
    class Mission
    {
        Random rgen = new Random();
        Item missionItem;
        int infamyLv;
        int contractCost;
        Rectangle rec;


        public Mission(Item MI)
        {
            missionItem = MI;
            contractCost = missionItem.Money / 2;
            infamyLv = missionItem.Infamy;


        }
        public void RandomRec(Rectangle rc)
        {
            rec = rc;
        }
        public Item MissionItem
        {
            get { return missionItem; }
        }
        public int InfamyLv
        {
            get { return infamyLv; }
        }
        public int ContractCost
        {
            get { return contractCost; }
        }


        public Rectangle Rec
        {
            get { return rec; }
            set { rec = value; }
        }
    }
}
