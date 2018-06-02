using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHallOfHeroes
{
    delegate void delCheckSkillDuration(GameEngine insGameEngine, Player insPlayer);
    class Game
    {
        static Game insGame = new Game();
        static GameEngine insGameEngine = new GameEngine();
        static Player insPlayer = new Player();
        static Barbarian insBarbarian = new Barbarian();
        static Wanderer insWanderer = new Wanderer();
        static Forcekeeper insForcekeeper = new Forcekeeper();

        public void MainMenu()
        {
            Console.Clear();
            insPlayer.arrayData[0].VictoryMeter = 0;
            insPlayer.arrayData[1].VictoryMeter = 0;
            #region MainGraphic
            Console.WriteLine();
            Console.WriteLine(" THE HALL");
            Console.WriteLine("    OF");
            Console.WriteLine("  HEROES");
            Console.WriteLine();
            #endregion
            Console.WriteLine("1 - Start!");
            Console.WriteLine("2 - Rules");
            Console.WriteLine("3 - About");
            Console.WriteLine("4 - Exit");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        Start();
                        IfKeyIsCorrect = true;
                        break;
                    case '2':
                        Rules();
                        IfKeyIsCorrect = true;
                        break;
                    case '3':
                        About();
                        IfKeyIsCorrect = true;
                        break;
                    case '4':
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);
            Console.ReadKey();
        }
        static void Start()
        {
            Console.Clear();
            for (int i = 1; i <= 2; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Player {0}:", i);
                insGameEngine.ActivePlayerID = i - 1;
                ClassChoise();
                Console.WriteLine();
                NameChoise();
                Console.WriteLine();
            }
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Player 1: " + insPlayer.arrayData[0].Name + " - " + insPlayer.arrayData[0].Class);
            Console.WriteLine("Player 2: " + insPlayer.arrayData[1].Name + " - " + insPlayer.arrayData[1].Class);
            Console.WriteLine();
            Console.WriteLine("1 - Start the battle!");
            Console.WriteLine("2 - Change name/class");
            Console.WriteLine("3 - Back");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        BattleWindow();
                        IfKeyIsCorrect = true;
                        break;
                    case '2':
                        Console.Clear();
                        Start();
                        IfKeyIsCorrect = true;
                        break;
                    case '3':
                        Console.Clear();
                        insGame.MainMenu();
                        IfKeyIsCorrect = true;
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);
            Console.ReadKey();
        }
        static void Rules()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Choose your hero class and stand to the battle!");
            Console.WriteLine("Use your unique skills to beat your opponent" + "\n" + "definitely as fast as you can!");
            Console.WriteLine();
            Console.WriteLine("HEROES CLASSES:");
            Console.WriteLine();
            Console.WriteLine("I.Barbarian");
            Console.WriteLine("Strong and muscular warrior who slits his enemies with great battle axe.");
            Console.WriteLine("Despite his effectiveness on hand-to-hand combat, he's weakness are distance duels.");
            Console.WriteLine();
            Console.WriteLine("   SKILLS:");
            Console.WriteLine("   1.Jump - use your muscle power to make a very long jump which" + "\n" + "   makes you stand a front of your opponent immediately.");
            Console.WriteLine("   2.Berserk - you can relase your rage to increase your Melee Damage" + "\n" + "   at the cost of your Defense which going to be decrased to 0. Lasts 3 moves.");
            Console.WriteLine("   3.Devastation - a powerfull hit which crushes your opponent's" + "\n" + "   armor and decreases his Defense for rest of the battle.");
            Console.WriteLine();
            Console.WriteLine("II.Wanderer");
            Console.WriteLine("A mysterious wayfarer who eliminates his enemies in a flash by his long bow.");
            Console.WriteLine("His weapon is useless in short distances but his insane speed allows him to" + "\n" + "pull out before every distance attack.");
            Console.WriteLine();
            Console.WriteLine("   SKILLS:");
            Console.WriteLine("   1.Heal - a fast drink of a healing potion which saved life a lot of times.");
            Console.WriteLine("   2.Eagle Eye - a while of concentration before every hit increases your Hit" + "\n" + "   and Critical Hit Chance. Lasts 3 moves.");
            Console.WriteLine("   3.Vampiric Pierce - a powerfull shot which pierces your enemy inside out." + "\n" + "   The mysterious power steals some of his life energy for you...");
            Console.WriteLine();
            Console.WriteLine("III.Forcekeeper");
            Console.WriteLine("A Elder of Secret Order, which commands a power of magic. His powerfull spells can" + "\n" + "turns to dust his opponent in a flash. His touch can steals a Mana of his enemy." + "\n" + "He can also teleports for short distance.");
            Console.WriteLine();
            Console.WriteLine("   SKILLS:");
            Console.WriteLine("   1.Sleep - use this spell to sleep your enemy for a while. Lasts 1 move.");
            Console.WriteLine("   2.Fire Shield - a spell which orders fire to protect you. Increases your defense" + "\n" + "   and gives damage to everybody who hits you. Lasts 3 moves.");
            Console.WriteLine("   3.Lightning - a powerfull spell which summons a lightning to hits your opponent." + "\n" + "   Has a large diversity of damage");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("TIPS:");
            Console.WriteLine();
            Console.WriteLine("- Watch your Health Points! When it diminishes to 0 then you lose the battle." + "\n" + "  Only Wanderer can heals himself.");
            Console.WriteLine("- Every skill uses required amount of Mana Points. Except Forcekeeper," + "\n" + "  nobody can restore them.");
            Console.WriteLine("- Distance between players determinates which attack is possible. If distance" + "\n" + "  is less than 2m, you can do melee attack, otherwise you can attack from distance.");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("1 - Back");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        insGame.MainMenu();
                        IfKeyIsCorrect = true;
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);

        }
        static void About()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("   The Hall of Heroes - version 1.0");
            Console.WriteLine();
            Console.WriteLine(" Copyright (c) Damian Szafraniec 2018");
            Console.WriteLine("       Email: dam.szaf@gmail.com");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1 - Back");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        insGame.MainMenu();
                        IfKeyIsCorrect = true;
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);
        }
        static void ClassChoise()
        {
            Console.WriteLine();
            Console.WriteLine("Choose your class!");
            Console.WriteLine("1 - Barbarian");
            Console.WriteLine("2 - Wanderer");
            Console.WriteLine("3 - Forcekeeper");
            Console.WriteLine("---------------");
            Console.WriteLine("4 - Back");
            bool IfKeyIsCorrect = false;
            do
            {
                ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                switch (KeyNumber.KeyChar)
                {
                    case '1':
                        insBarbarian.Parameters(insPlayer, insGameEngine.ActivePlayerID);
                        IfKeyIsCorrect = true;
                        break;
                    case '2':
                        insWanderer.Parameters(insPlayer, insGameEngine.ActivePlayerID);
                        IfKeyIsCorrect = true;
                        break;
                    case '3':
                        insForcekeeper.Parameters(insPlayer, insGameEngine.ActivePlayerID);
                        IfKeyIsCorrect = true;
                        break;
                    case '4':
                        Console.Clear();
                        insGame.MainMenu();
                        IfKeyIsCorrect = true;
                        break;
                    default:
                        break;
                }
            } while (IfKeyIsCorrect == false);
        }
        static void NameChoise()
        {
            Console.WriteLine("Set your name:");
            Console.CursorVisible = true;
            insPlayer.SetName(insPlayer, insGameEngine, Console.ReadLine());
            Console.CursorVisible = false;
        }
        static void BattleWindow()
        {
            Console.Clear();
            insGameEngine.stackBattleInfo.Clear();
            insBarbarian.queueRoundMeter.Clear();
            insWanderer.queueRoundMeter.Clear();
            insForcekeeper.queueRoundMeter.Clear();
            Random random = new Random();
            insGameEngine.ActivePlayerID = random.Next(0, 2);
            for (int i = 0; i <= 1; i++)
            {
                insGameEngine.arrayCurrent[i].CurrentHealthPoints = insPlayer.arrayData[i].HealthPoints;
                insGameEngine.arrayCurrent[i].CurrentManaPoints = insPlayer.arrayData[i].ManaPoints;
            }
            insGameEngine.DistanceBetweenPlayers = 15; // PARAMETER
            insGameEngine.RoundMeter = 1;
            insGameEngine.ShowCurrentPlayerInfo(insPlayer);
            Console.WriteLine("Your turn, {0}!", insPlayer.arrayData[insGameEngine.ActivePlayerID].Name);
            Console.WriteLine("Distance between players: " + insGameEngine.DistanceBetweenPlayers + "m");
            Console.WriteLine("Round: " + insGameEngine.RoundMeter);
            Console.WriteLine();
            insGameEngine.ShowBattleOptions(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
        }
        public void NextMove(Player insPlayer)
        {
            if (insGameEngine.IfSomebodyWon()) System.Threading.Thread.Sleep(2000);
            Console.Clear();
            delCheckSkillDuration CheckSkillDuration = new delCheckSkillDuration(insBarbarian.CheckSkillDuration);
            CheckSkillDuration += insWanderer.CheckSkillDuration;
            CheckSkillDuration += insForcekeeper.CheckSkillDuration;
            if (insGameEngine.IfSomebodyWon() == false)
            {
                insGameEngine.ActivePlayerID = (insGameEngine.ActivePlayerID == 0 ? 1 : 0);
                insGameEngine.RoundMeter++;
                CheckSkillDuration(insGameEngine, insPlayer);
                insGameEngine.ShowCurrentPlayerInfo(insPlayer);
                Console.WriteLine("Your turn, {0}!", insPlayer.arrayData[insGameEngine.ActivePlayerID].Name);
                Console.WriteLine("Distance between players: " + insGameEngine.DistanceBetweenPlayers + "m");
                Console.WriteLine("Round: " + insGameEngine.RoundMeter);
                Console.WriteLine();
                insForcekeeper.CheckSleep(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
            }
            else
            {
                insPlayer.arrayData[insGameEngine.ActivePlayerID].VictoryMeter += 1;
                Console.WriteLine();
                Console.WriteLine("The battle has ended! The winner is {0}", insPlayer.arrayData[insGameEngine.ActivePlayerID].Name);
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("1 - Revenge");
                Console.WriteLine("2 - Main menu");
                Console.WriteLine("3 - Exit");
                bool IfKeyIsCorrect = false;
                do
                {
                    ConsoleKeyInfo KeyNumber = Console.ReadKey(true);
                    switch (KeyNumber.KeyChar)
                    {
                        case '1':
                            BattleWindow();
                            IfKeyIsCorrect = true;
                            break;
                        case '2':
                            MainMenu();
                            IfKeyIsCorrect = true;
                            break;
                        case '3':
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                } while (IfKeyIsCorrect == false);
            }

        }
        public void SameMove()
        {
            Console.Clear();
            insGameEngine.ShowCurrentPlayerInfo(insPlayer);
            Console.WriteLine("Your turn, {0}!", insPlayer.arrayData[insGameEngine.ActivePlayerID].Name);
            Console.WriteLine("Distance between players: " + insGameEngine.DistanceBetweenPlayers + "m");
            Console.WriteLine("Round: " + insGameEngine.RoundMeter);
            Console.WriteLine();
            insGameEngine.ShowBattleOptions(insGame, insGameEngine, insPlayer, insBarbarian, insWanderer, insForcekeeper);
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "The Hall of Heroes, version 1.0";
            insGame.MainMenu();
        }
    }
}
