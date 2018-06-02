using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    interface IClass
    {
        void CheckSkillDuration(GameEngine insGameEngine, Player insPlayer);
        void Parameters(Player insPlayer, int ActivePlayerID);
        void Skill1(Game insGame, GameEngine insGameEngine, Player insPlayer);
        void Skill2(Game insGame, GameEngine insGameEngine, Player insPlayer);
        void Skill3(Game insGame, GameEngine insGameEngine, Player insPlayer);
    }
    public enum EnumClass { Barbarian=0, Wanderer=1, Forcekeeper = 2 };
    struct PlayerData
    {
        public string Name { get; set; }
        public EnumClass Class { get; set; }
        public int HealthPoints { get; set; }
        public int ManaPoints { get; set; }
        public int MeleeDamage { get; set; }
        public int DistanceDamage { get; set; }
        public int Defense { get; set; }
        public int HitChance { get; set; }
        public int CriticalChance { get; set; }
        public int MoveSpeed { get; set; }
        public bool IfSkillIsActive { get; set; }
        public int VictoryMeter { get; set; }
    }
    class Player
    {
        public PlayerData[] arrayData = new PlayerData[2];
        public void SetName(Player insPlayer, GameEngine insGameEngine, string Name)
        {
            insPlayer.arrayData[insGameEngine.ActivePlayerID].Name = Name;
        }
    }
}
