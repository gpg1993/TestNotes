using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Flyweight
{
    public class SoldierFactory
    {
        private static readonly SoldierFactory instance = new SoldierFactory();
        private static Hashtable ht;
        private SoldierFactory()
        {
            ht = new Hashtable();
            RedSoldier redSoldier = new RedSoldier();
            ht.Add(StandType.red, redSoldier);
            BlueSoldier blueSoldier = new BlueSoldier();
            ht.Add(StandType.blue, blueSoldier);
        }

        public static SoldierFactory GetInstance()
        {
            return instance;
        }

        public Soldier GetSoldier(StandType standType)
        {
            Soldier soldier = ht[standType] as Soldier;
            return soldier;
        }

    }
}
