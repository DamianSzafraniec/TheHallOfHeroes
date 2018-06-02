using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    class Wanderer : IClass
    {
        public Queue<int> queueRoundMeter = new Queue<int>();
        public void CheckSkillDuration(GameEngine insGameEngine, Player insPlayer)
        {
            if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == true &&
                insPlayer.arrayData[insGameEngine.ActivePlayerID].Class == EnumClass.Wanderer)
            {
                queueRoundMeter.Enqueue(insGameEngine.RoundMeter);
                if (insGameEngine.RoundMeter >= (queueRoundMeter.Peek() + 4))
                {
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].HitChance = 80; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].CriticalChance = 6; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = false;
                    queueRoundMeter.Clear();
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + ", your Eagle Eye effect has ended.");
                }
            }
        }
        public void Parameters(Player insPlayer, int ActivePlayerID)
        {
            insPlayer.arrayData[ActivePlayerID].Class = EnumClass.Wanderer;
            insPlayer.arrayData[ActivePlayerID].HealthPoints = 210; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].ManaPoints = 120; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MeleeDamage = 8; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].DistanceDamage = 35; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].Defense = 50; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].HitChance = 80; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].CriticalChance = 6; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MoveSpeed = 2; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].IfSkillIsActive = false;
        }
        public void Skill1(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            int RequiredManaPoints = 15; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                 if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints < insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints)
                 {
                     insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Heal.");
                     insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                     insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints += 10; // PARAMETER
                }
                 else insGameEngine.stackBattleInfo.Push("You already have full of health points!");
                 if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints > insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints)
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints = insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints;
                insGame.SameMove();
            }
            else
            {
                insGameEngine.stackBattleInfo.Push("Not enough Mana!");
                insGame.SameMove();
            }
        }
        public void Skill2(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            int RequiredManaPoints = 30; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == false)
                {
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Eagle Eye.");
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = true;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].HitChance += 18; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].CriticalChance += 30; // PARAMETER
                    insGame.SameMove();
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("The Eagle Eye is already active!");
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
            int RequiredManaPoints = 40; // PARAMETER
            int Damage = random.Next(35, 65); // PARAMETER
            double StolenHP = Math.Round(0.35*Damage); // PARAMETER
            int OpponentPlayerID = (insGameEngine.ActivePlayerID == 0 ? 1 : 0);
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insGameEngine.DistanceBetweenPlayers > 2)
                {
                    if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints < 
                        insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints)
                    {
                        insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Vampiric Pierce (" +
                            Damage + " damage). " + (int)(Damage*0.35) +  " HP has been stolen."); // PARAMETER
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                        insGameEngine.arrayCurrent[OpponentPlayerID].CurrentHealthPoints -= Damage;
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints += (int)StolenHP;
                    }
                    else
                    {
                        insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Vampiric Pierce (" +
                            Damage + " damage).");
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                        insGameEngine.arrayCurrent[OpponentPlayerID].CurrentHealthPoints -= Damage;
                    }
                    if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints > insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints)
                        insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentHealthPoints = insPlayer.arrayData[insGameEngine.ActivePlayerID].HealthPoints;
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("You cannot use Vampiric Pierce standing closer than 2m!");
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
