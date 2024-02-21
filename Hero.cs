using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_01
{
    internal class Hero
    {
        private string name;
        private int health;
        private int attackPower;
        private double resistanceToPhysical;
        private double resistanceToMagical;
        private double criticalChance;
        private double dodgeChance;

        public Hero(string name, int health, int attackPower, double resistanceToPhysical, double resistanceToMagical, double criticalChance, double dodgeChance)
        {
            this.name = name;
            this.health = health;
            this.attackPower = attackPower;
            this.resistanceToPhysical = resistanceToPhysical;
            this.resistanceToMagical = resistanceToMagical;
            this.criticalChance = criticalChance;
            this.dodgeChance = dodgeChance;
        }
    }
}
