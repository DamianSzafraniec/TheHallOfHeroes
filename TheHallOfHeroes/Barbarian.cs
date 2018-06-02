using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    class Barbarian : IClass
    {
        public Queue<int> queueRoundMeter = new Queue<int>();
        public void CheckSkillDuration(GameEngine insGameEngine, Player insPlayer)
        {
            if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == true && 
                insPlayer.arrayData[insGameEngine.ActivePlayerID].Class == EnumClass.Barbarian)
            {
                queueRoundMeter.Enqueue(insGameEngine.RoundMeter);
                if (insGameEngine.RoundMeter >= (queueRoundMeter.Peek() + 4))
                {
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].MeleeDamage = 35; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].Defense = 100; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = false;
                    queueRoundMeter.Clear();
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + ", your Berserk effect has ended.");
                }
            }
        }
        public void Parameters(Player insPlayer, int ActivePlayerID)
        {
            insPlayer.arrayData[ActivePlayerID].Class = EnumClass.Barbarian;
            insPlayer.arrayData[ActivePlayerID].HealthPoints = 260; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].ManaPoints = 100; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MeleeDamage = 30; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].DistanceDamage = 10; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].Defense = 80; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].HitChance = 85; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].CriticalChance = 3; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MoveSpeed = 3; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].IfSkillIsActive = false;
        }
        public void Skill1(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            int RequiredManaPoints = 10; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insGameEngine.DistanceBetweenPlayers != 0)
                {
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Jump.");
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                    insGameEngine.DistanceBetweenPlayers = 0;
                    insGame.SameMove();
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("You already stand front of your opponent!");
                    insGame.SameMove();
                }
            }
            else
            {
                insGameEngine.stackBattleInfo.Push("Not enough Mana!");
                insGame.SameMove();
            }
        }
        public void Skill2(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            int RequiredManaPoints = 20; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == false)
                {
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Berserk.");
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = true;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].MeleeDamage += 15; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].Defense = 0; // PARAMETER
                    insGame.SameMove();
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("The Berserk is already active!");
                    insGame.SameMove();
                }
            }
            else
            {
                insGameEngine.stackBattleInfo.Push("Not enough Mana!");
                insGame.SameMove();
            }
        }
        public void Skill3(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            Random random = new Random();
            int RequiredManaPoints = 30; // PARAMETER
            int Damage = random.Next(50, 80); // PARAMETER
            int OpponentPlayerID = (insGameEngine.ActivePlayerID == 0 ? 1 : 0);
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insGameEngine.DistanceBetweenPlayers <= 2)
                {
                    if (insPlayer.arrayData[OpponentPlayerID].Defense > 0)
                    {
                        insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Devastation (" +
                            Damage + " damage). The opponent's defense decreased.");
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                        insGameEngine.arrayCurrent[OpponentPlayerID].CurrentHealthPoints -= Damage;
                        insPlayer.arrayData[OpponentPlayerID].Defense -= 20; // PARAMETER
                    }
                    else
                    {
                        insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Devastation (" +
                            Damage + " damage).");
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                        insGameEngine.arrayCurrent[OpponentPlayerID].CurrentHealthPoints -= Damage;
                    }
                    if (insPlayer.arrayData[OpponentPlayerID].Defense < 0) insPlayer.arrayData[OpponentPlayerID].Defense = 0;
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("You cannot use Devastation from distance over 2m!");
                    insGame.SameMove();
                }
            }
            else
            {
                insGameEngine.stackBattleInfo.Push("Not enough Mana!");
                insGame.SameMove();
            }
        }
    }
}
