using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    public struct CurrentPlayerInfo
    {
        public int CurrentHealthPoints { get; set; }
        public int CurrentManaPoints { get; set; }
        public int CurrentPosition { get; set; }
    }
    class GameEngine
    {
        public CurrentPlayerInfo[] arrayCurrent = new CurrentPlayerInfo[2];
        public Stack stackBattleInfo = new Stack();
        public int ActivePlayerID { get; set; }
        public int DistanceBetweenPlayers { get; set; }
        public int RoundMeter { get; set; }
        public bool IfSomebodyWon()
        {
            if (arrayCurrent[0].CurrentHealthPoints <= 0 || arrayCurrent[1].CurrentHealthPoints <= 0) return true;
            else return false;
        }
        public void ShowCurrentPlayerInfo(Player insPlayer)
        {
            Console.WriteLine();
            Console.WriteLine("Player 1 - {0}", insPlayer.arrayData[0].Name);
            Console.WriteLine(insPlayer.arrayData[0].Class);
            Console.WriteLine("HP: {0}/{1}", arrayCurrent[0].CurrentHealthPoints, insPlayer.arrayData[0].HealthPoints);
            Console.WriteLine("MP: {0}/{1}", arrayCurrent[0].CurrentManaPoints, insPlayer.arrayData[0].ManaPoints);
            Console.WriteLine("Victories: " + insPlayer.arrayData[0].VictoryMeter);
            Console.WriteLine();
            Console.WriteLine("Player 2 - {0}", insPlayer.arrayData[1].Name);
            Console.WriteLine(insPlayer.arrayData[1].Class);
            Console.WriteLine("HP: {0}/{1}", arrayCurrent[1].CurrentHealthPoints, insPlayer.arrayData[1].HealthPoints);
            Console.WriteLine("MP: {0}/{1}", arrayCurrent[1].CurrentManaPoints, insPlayer.arrayData[1].ManaPoints);
            Console.WriteLine("Victories: " + insPlayer.arrayData[1].VictoryMeter);
            Console.WriteLine();
        }
        public void ShowBattleOptions(Game insGame, GameEngine insGameEngine, Player insPlayer, Barbarian insBarbarian, Wanderer insWanderer, Forcekeeper insForcekeeper)
        {
            #region ProgressOfTheBattle
            object[] arrayTempBattleInfo = stackBattleInfo.ToArray();
            Console.WriteLine("                         PROGRESS OF THE BATTLE");
            Console.WriteLine("--------------------------------------------------------------------------");
            switch (stackBattleInfo.Count)
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
            switch (insPlayer.arrayData[insGameEngine.ActivePlayerID].Class)
            {
                case EnumClass.Barbarian:
                    {
                        Console.WriteLine("1 - Melee attack (Battle Axe)");
                        Console.WriteLine("2 - Distance attack (Throwing Axes)");
                        Console.WriteLine("3 - Jump (10 MP)");
                        Console.WriteLine("4 - Berserk (20 MP)");
                        Console.WriteLine("5 - Devastation (30 MP)");
                    }
                    break;
                case EnumClass.Wanderer:
                    {
                        Console.WriteLine("1 - Melee attack (Dagger)");
                        Console.WriteLine("2 - Distance attack (Long Bow)");
                        Console.WriteLine("3 - Heal (15 MP)");
                        Console.WriteLine("4 - Eagle Eye (30 MP)");
                        Console.WriteLine("5 - Vampiric Pierce (40 MP)");
                    }
                    break;
                case EnumClass.Forcekeeper:
                    {
                        Console.WriteLine("1 - Melee attack (Sinister Touch)");
                        Console.WriteLine("2 - Distance attack (Bone Wand)");
                        Console.WriteLine("3 - Sleep (25 MP)");
                        Console.WriteLine("4 - Fire Shield (40 MP)");
                        Console.WriteLine("5 - Lightning (100 MP)");
                    }
                    break;
            }
            Console.WriteLine("6 - Move forward");
            Console.WriteLine("7 - Move back");
            Console.WriteLine("8 - Pass the turn");
            Console.WriteLine("------------------");
            Console.WriteLine("9 - End the game");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        MeeleAttack(insGame, insPlayer);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '2':
                        DistanceAttack(insGame, insPlayer);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '3':
                        Skill1(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '4':
                        Skill2(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '5':
                        Skill3(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '6':
                        MoveForward(insGame, insPlayer);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '7':
                        MoveBack(insGame, insPlayer);
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '8':
                        insGameEngine.stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " passed the turn.");
                        insGame.NextMove(insPlayer);
                        IfKeyIsCorrect = true;
                        break;
                    case '9':
                        Console.Clear();
                        insGame.MainMenu();
                        IfKeyIsCorrect = true;
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);
        }
        void MeeleAttack(Game insGame, Player insPlayer)
        {
            if (DistanceBetweenPlayers <= 2)
            {
                Random random = new Random();
                int DamageRate = random.Next(90, 110); // PARAMETER
                int DefenseRate = random.Next(10, 20); // PARAMETER
                int Miss = random.Next(0, 100);
                int Critical = random.Next(0, 100);
                int OpponentPlayerID = (ActivePlayerID == 0 ? 1 : 0);
                int Damage = (insPlayer.arrayData[ActivePlayerID].MeleeDamage * DamageRate / 100
                    - insPlayer.arrayData[OpponentPlayerID].Defense * DefenseRate / 100);
                if (Damage < 0) Damage = 0;
                int FireShieldDamage = random.Next(25, 35);  // PARAMETER
                int StolenManaPoints = 15;  // PARAMETER
                if (Miss < insPlayer.arrayData[ActivePlayerID].HitChance)
                {
                    if (Critical > insPlayer.arrayData[ActivePlayerID].CriticalChance)
                    {
                        arrayCurrent[OpponentPlayerID].CurrentHealthPoints = arrayCurrent[OpponentPlayerID].CurrentHealthPoints - Damage;
                        stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " deals " + Damage + " damage.");
                    }
                    else
                    {
                        arrayCurrent[OpponentPlayerID].CurrentHealthPoints = arrayCurrent[OpponentPlayerID].CurrentHealthPoints - (2*Damage);
                        stackBattleInfo.Push("CRITICAL HIT! " + insPlayer.arrayData[ActivePlayerID].Name + " deals " + (2*Damage) + " damage.");
                    }
                    if (insPlayer.arrayData[OpponentPlayerID].IfSkillIsActive == true && insPlayer.arrayData[OpponentPlayerID].Class == EnumClass.Forcekeeper)
                    {
                        arrayCurrent[ActivePlayerID].CurrentHealthPoints -= FireShieldDamage;
                        stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " takes " + FireShieldDamage + " damage from Fire Shield.");
                    }
                    if (insPlayer.arrayData[ActivePlayerID].Class == EnumClass.Forcekeeper &&
                        arrayCurrent[ActivePlayerID].CurrentManaPoints < insPlayer.arrayData[ActivePlayerID].ManaPoints &&
                        arrayCurrent[OpponentPlayerID].CurrentManaPoints > 0)
                    {
                        stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " has stolen Mana Points.");
                        arrayCurrent[ActivePlayerID].CurrentManaPoints += StolenManaPoints;
                        arrayCurrent[OpponentPlayerID].CurrentManaPoints -= StolenManaPoints;
                    }
                    if (arrayCurrent[ActivePlayerID].CurrentManaPoints > insPlayer.arrayData[ActivePlayerID].ManaPoints)
                        arrayCurrent[ActivePlayerID].CurrentManaPoints = insPlayer.arrayData[ActivePlayerID].ManaPoints;
                    if (arrayCurrent[OpponentPlayerID].CurrentManaPoints < 0)
                        arrayCurrent[OpponentPlayerID].CurrentManaPoints = 0;
                }
                else
                {
                    stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " missed.");
                    insGame.NextMove(insPlayer);
                }
            }
            else
            {
                stackBattleInfo.Push("You cannot do melee attack from distance over 2m!");
                insGame.SameMove();
            }
        }
        void DistanceAttack(Game insGame, Player insPlayer)
        {
            if (insPlayer.arrayData[ActivePlayerID].Class == EnumClass.Wanderer && DistanceBetweenPlayers < 8)
            {
                stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " moves back.");
                DistanceBetweenPlayers += 4;  // PARAMETER
                if (DistanceBetweenPlayers >= 100) DistanceBetweenPlayers = 100;
            }
            if (DistanceBetweenPlayers > 2)
            {
                Random random = new Random();
                int DamageRate = random.Next(85, 115);  // PARAMETER
                int DefenseRate = random.Next(8, 16);  // PARAMETER
                int Miss = random.Next(0, 100);
                int Critical = random.Next(0, 100);
                int OpponentPlayerID = (ActivePlayerID == 0 ? 1 : 0);
                int Damage = (insPlayer.arrayData[ActivePlayerID].DistanceDamage * DamageRate / 100
                - insPlayer.arrayData[OpponentPlayerID].Defense * DefenseRate / 100);
                if (Damage < 0) Damage = 0;
                if (Miss < insPlayer.arrayData[ActivePlayerID].HitChance)
                {
                    if (Critical > insPlayer.arrayData[ActivePlayerID].CriticalChance)
                    {
                        arrayCurrent[OpponentPlayerID].CurrentHealthPoints = arrayCurrent[OpponentPlayerID].CurrentHealthPoints - Damage;
                        stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " deals " + Damage + " damage.");
                    }
                    else
                    {
                        arrayCurrent[OpponentPlayerID].CurrentHealthPoints = arrayCurrent[OpponentPlayerID].CurrentHealthPoints - (2*Damage);
                        stackBattleInfo.Push("CRITICAL HIT! " + insPlayer.arrayData[ActivePlayerID].Name + " deals " + (2*Damage) + " damage.");
                    }
                }
                else
                {
                    stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " missed.");
                    insGame.NextMove(insPlayer);
                }

            }
            else
            {
                stackBattleInfo.Push("You cannot do distance attack standing closer than 2m!");
                insGame.SameMove();
            }
        }
        void Skill1(Game insGame, GameEngine insGameEngine, Player insPlayer, Barbarian insBarbarian, Wanderer insWanderer, Forcekeeper insForcekeeper)
        {
            switch (insPlayer.arrayData[ActivePlayerID].Class)
            {
                case EnumClass.Barbarian:
                    insBarbarian.Skill1(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Wanderer:
                    insWanderer.Skill1(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Forcekeeper:
                    insForcekeeper.Skill1(insGame, insGameEngine, insPlayer);
                    break;
            }
        }
        void Skill2(Game insGame, GameEngine insGameEngine, Player insPlayer, Barbarian insBarbarian, Wanderer insWanderer, Forcekeeper insForcekeeper)
        {
            switch (insPlayer.arrayData[ActivePlayerID].Class)
            {
                case EnumClass.Barbarian:
                    insBarbarian.Skill2(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Wanderer:
                    insWanderer.Skill2(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Forcekeeper:
                    insForcekeeper.Skill2(insGame, insGameEngine, insPlayer);
                    break;
            }
        }
        void Skill3(Game insGame, GameEngine insGameEngine, Player insPlayer, Barbarian insBarbarian, Wanderer insWanderer, Forcekeeper insForcekeeper)
        {
            switch (insPlayer.arrayData[ActivePlayerID].Class)
            {
                case EnumClass.Barbarian:
                    insBarbarian.Skill3(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Wanderer:
                    insWanderer.Skill3(insGame, insGameEngine, insPlayer);
                    break;
                case EnumClass.Forcekeeper:
                    insForcekeeper.Skill3(insGame, insGameEngine, insPlayer);
                    break;
            }
        }
        void MoveForward(Game insGame, Player insPlayer)
        {
            if (DistanceBetweenPlayers == 0)
            {
                stackBattleInfo.Push("You already stand front of your opponent!");
                insGame.SameMove();
            }
            if (DistanceBetweenPlayers > 0)
            {
                stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " moves forward.");
                DistanceBetweenPlayers -= insPlayer.arrayData[ActivePlayerID].MoveSpeed;
            }
            if (DistanceBetweenPlayers <= 0)
            {
                stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " moves forward.");
                DistanceBetweenPlayers = 0;
            }
        }
        void MoveBack(Game insGame, Player insPlayer)
        {
            if (DistanceBetweenPlayers == 100)
            {
                stackBattleInfo.Push("You cannot retreat further than 100m!");
                insGame.SameMove();
            }
            if (DistanceBetweenPlayers < 100)
            {
                stackBattleInfo.Push(insPlayer.arrayData[ActivePlayerID].Name + " moves back.");
                DistanceBetweenPlayers += insPlayer.arrayData[ActivePlayerID].MoveSpeed;
            }
            if (DistanceBetweenPlayers >= 100) DistanceBetweenPlayers = 100;
        }
    }
}
