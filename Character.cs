using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class Character
    {
        public int hp {
            get;
            private set;
        }

        public int maxHp
        {
            get;
            private set;
        }
        
        public int mp
        {
            get;
            private set;
        }
        public int maxMp { 
            get; 
            private set; 
        }
        public int attack { 
            get; 
            private set; 
        }
        public string name { 
            get;  
        }

        public string AA { 
            get; 
            set; 
        }
        public int command { 
            get; 
            set; 
        }
        public int target { 
            get; 
            set; 
        }



        public Character(int maxHp,int maxMp, int attack, string name, string AA)
        {
            this.hp = maxHp;
            this.maxHp = maxHp;
            this.mp = maxMp;
            this.maxMp = maxMp;
            this.attack = attack;
            this.name = name;
            this.AA = AA;
        }

        public void Attacked(int damage)
        {
            this.hp -= damage;
            if (this.hp <= 0)
            {
                this.hp = 0;
            }
        }
        public bool HasKnockedDown()
        {
            return this.hp == 0;
        }


        public bool IsMPEnough()
        {
            return this.hp >= Constant.SPELL_COST;
        }
        public void Heal()
        {
            Debug.Assert(IsMPEnough());
            this.mp -= Constant.SPELL_COST;
            this.hp = this.maxHp;
        }
    }
}
