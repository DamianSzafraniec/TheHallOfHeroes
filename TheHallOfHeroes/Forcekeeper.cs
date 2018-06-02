using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    class Forcekeeper : IClass
    {
        public Queue<int> queueRoundMeter = new Queue<int>();
        public bool IfSleepIsActive = false;
        public void CheckSleep(Game insGame, GameEngine insGameEngine, Player insPlayer, Barbarian insBarbarian, Wanderer insWanderer, Forcekeeper insForcekeeper)
        {
            if (IfSleepIsActive == false)
                insGameEngine.ShowBattleOptions(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
            else
            {
                insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " is sleeping...");
                #region ProgressOfTheBattle
                object[] arrayTempBattleInfo = insGameEngine.stackBattleInfo.ToArray();
                Console.WriteLine("                         PROGRESS OF THE BATTLE");
                Console.WriteLine("--------------------------------------------------------------------------");
                switch (insGameEngine.stackBattleInfo.Count)
                {
                    case 0:
                        Console.WriteLine("\n" + "\n" + "\n" + "\n");
                        break;
                    case 1:
                        Console.WriteLine(arrayTempBattleInfo[0]);
                        Console.WriteLine("\n" + "\n" + "\n");
                        break;
                    case 2:
                        for (int i = 0; i <= 1; i++) Console.WriteLine(arrayTempBattleInfo[i]);
                        Console.WriteLine("\n" + "\n");
                        break;
                    case 3:
                        for (int i = 0; i <= 2; i++) Console.WriteLine(arrayTempBattleInfo[i]);
                        Console.WriteLine("\n");
                        break;
                    case 4:
                        for (int i = 0; i <= 3; i++) Console.WriteLine(arrayTempBattleInfo[i]);
                        Console.WriteLine();
                        break;
                    default:
                        for (int i = 0; i <= 4; i++) Console.WriteLine(arrayTempBattleInfo[i]);
                        break;
                }
                Console.WriteLine("--------------------------------------------------------------------------");
                #endregion
                System.Threading.Thread.Sleep(5000); // PARAMETER
                insForcekeeper.IfSleepIsActive = false;
                insGame.NextMove(insPlayer);
            }
        }
        public void CheckSkillDuration(GameEngine insGameEngine, Player insPlayer)
        {
            if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == true &&
                insPlayer.arrayData[insGameEngine.ActivePlayerID].Class == EnumClass.Forcekeeper)
            {
                queueRoundMeter.Enqueue(insGameEngine.RoundMeter);
                if (insGameEngine.RoundMeter >= (queueRoundMeter.Peek() + 4))
                {
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].Defense = 20; // PARAMETER
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = false;
                    queueRoundMeter.Clear();
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + ", your Fire Shield effect has ended.");
                }
            }
        }
        public void Parameters(Player insPlayer, int ActivePlayerID)
        {
            insPlayer.arrayData[ActivePlayerID].Class = EnumClass.Forcekeeper;
            insPlayer.arrayData[ActivePlayerID].HealthPoints = 180; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].ManaPoints = 290; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MeleeDamage = 25; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].DistanceDamage = 25; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].Defense = 30; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].HitChance = 98; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].CriticalChance = 6; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].MoveSpeed = 20; // PARAMETER
            insPlayer.arrayData[ActivePlayerID].IfSkillIsActive = false;
        }
        public void Skill1(Game insGame, GameEngine insGameEngine, Player insPlayer)
        {
            int RequiredManaPoints = 25; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (IfSleepIsActive == false)
                {
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Sleep.");
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                    IfSleepIsActive = true;
                    insGame.SameMove();
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("The opponent is already sleeping!");
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
            int RequiredManaPoints = 40; // PARAMETER
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                if (insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive == false)
                {
                    insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Fire Shield.");
                    insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].IfSkillIsActive = true;
                    insPlayer.arrayData[insGameEngine.ActivePlayerID].Defense += 75; // PARAMETER
                    insGame.SameMove();
                }
                else
                {
                    insGameEngine.stackBattleInfo.Push("The Fire Shield is already active!");
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
            int RequiredManaPoints = 100; // PARAMETER
            int Damage = random.Next(1, 200); // PARAMETER
            int OpponentPlayerID = (insGameEngine.ActivePlayerID == 0 ? 1 : 0);
            if (insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints >= RequiredManaPoints)
            {
                insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[insGameEngine.ActivePlayerID].Name + " used Lightning (" +
                    Damage + " damage).");
                insGameEngine.arrayCurrent[insGameEngine.ActivePlayerID].CurrentManaPoints -= RequiredManaPoints;
                insGameEngine.arrayCurrent[OpponentPlayerID].CurrentHealthPoints -= Damage;
            }
            else
            {
                insGameEngine.stackBattleInfo.Push("Not enough Mana!");
                insGame.SameMove();
            }
        }
    }
}