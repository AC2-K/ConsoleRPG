using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace ConsoleRPG
{
    enum MONSTER
    {
        Player,
        Slime,
        Boss,
        MAX
    }

    enum CHARACTER
    {
        Player,
        Monster,
        MAX
    }


    enum COMMAND
    {
        Fight,
        Spell,
        Run,
        MAX
    }

    internal class Program
    {
        private static Random rng = new Random();
        private static Character[] monsters = new Character[(int)MONSTER.MAX];
        private static Character[] characters = new Character[(int)CHARACTER.MAX];
        private static string[] commandNames = new string[(int)COMMAND.MAX] {
            "たたかう",
            "じゅもん",
            "にげる"
        };
        static void Main(string[] args)
        {
            {
                /**
                monsters[(int)MONSTER.Player].hp = 15;
                monsters[(int)MONSTER.Player].maxHp = 15;
                monsters[(int)MONSTER.Player].mp = 15;
                monsters[(int)MONSTER.Player].maxMp = 15;
                monsters[(int)MONSTER.Player].attack = 3;
                monsters[(int)MONSTER.Player].name = "ゆうしゃ";
                /**/
                monsters[(int)MONSTER.Player] = new Character(20, 15, 5, "ゆうしゃ", "");
            }

            {
                /**
                monsters[(int)MONSTER.Slime].hp = 3;
                monsters[(int)MONSTER.Slime].maxHp = 3;
                monsters[(int)MONSTER.Slime].mp = 0;
                monsters[(int)MONSTER.Slime].maxMp = 0;
                monsters[(int)MONSTER.Slime].name = "スライム";
                monsters[(int)MONSTER.Slime].attack = 3;
                monsters[(int)MONSTER.Slime].AA = "／・Д・＼\n～～～～～";
                /**/
                
                monsters[(int)MONSTER.Slime] = new Character(3,0,3, "スライム", "／・Д・＼\n～～～～～");
            }


            {
                /**
                monsters[(int)MONSTER.Boss].hp = 255;
                monsters[(int)MONSTER.Boss].maxHp = 255;
                monsters[(int)MONSTER.Boss].mp = 0;
                monsters[(int)MONSTER.Boss].attack = 50;
                monsters[(int)MONSTER.Boss].maxHp = 0;
                monsters[(int)MONSTER.Boss].name = "魔王";
                monsters[(int)MONSTER.Boss].AA = "    A@A\nΦ （▼Ⅲ▼) Φ";
                /**/

                monsters[(int)MONSTER.Boss] = new Character(255, 0, 25, "魔王", "    A@A\nΦ （▼Ⅲ▼) Φ");
            }

            Init();
            Battle((int)MONSTER.Slime);
            Battle((int)MONSTER.Boss);
        }

        static void Init()
        {
            characters[(int)CHARACTER.Player] = monsters[(int)MONSTER.Player];
        }

        static void Battle(int _monster)
        {
            characters[(int)CHARACTER.Monster] = monsters[_monster];
            characters[(int)CHARACTER.Player].target = (int)CHARACTER.Monster;

            DrawBattleScreen();

            while (true)
            {
                SelectCommand();
                DrawBattleScreen();


                for (int i = 0; i < (int)CHARACTER.MAX; ++i)
                {
                    switch (characters[i].command)
                    {
                        case (int)COMMAND.Fight:
                            {
                                Console.WriteLine($"{characters[i].name}の攻撃");

                                Console.ReadKey();

                                int damege = rng.Next(1, characters[i].attack);
                                characters[characters[i].target].Attacked(damege);
                                DrawBattleScreen();

                                Console.WriteLine($"{characters[characters[i].target].name} に {damege}のダメージ!");
                                Console.ReadKey();
                                break;
                            }


                        case (int)COMMAND.Spell:
                            {
                                if (!characters[i].IsMPEnough())
                                {
                                    Console.WriteLine("MP がたりない！");
                                    Console.ReadKey();
                                    break;
                                }


                                Console.WriteLine($"{characters[i].name} は ヒール を唱えた");
                                Console.ReadKey();
                                characters[i].Heal();
                                DrawBattleScreen();

                                Console.WriteLine($"{characters[i].name} は回復した");
                                Console.ReadKey();
                            }
                            break;

                        case (int)COMMAND.Run:
                            {
                                Console.WriteLine($"{characters[i].name}は にげだした!!!");
                                Console.ReadKey();
                                return;
                            }
                    }
                   
                    if (characters[characters[i].target].HasKnockedDown())
                    {
                        switch (characters[i].target)
                        {
                            case (int)CHARACTER.Monster:
                                characters[characters[i].target].AA = "";
                                DrawBattleScreen();
                                Console.WriteLine();
                                Console.Clear();
                                Console.WriteLine($"{characters[characters[i].target].name}を たおした!");
                                break;
                            case (int)CHARACTER.Player:
                                Console.Clear();
                                DrawBattleScreen();
                                Console.Write("あなたは しにました");
                                break;
                            default:
                                break;
                        }
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }

        static void DrawBattleScreen()
        {
            Console.Clear();
            Console.WriteLine(characters[(int)CHARACTER.Player].name);
            Console.WriteLine($"HP {(int)characters[(int)CHARACTER.Player].hp}/{characters[(int)CHARACTER.Player].maxHp}    MP {(int)characters[(int)CHARACTER.Player].mp}/{characters[(int)CHARACTER.Player].maxMp}");
            Console.WriteLine("");

            Console.WriteLine($"{characters[(int)CHARACTER.Monster].AA}     HP:({characters[(int)CHARACTER.Monster].hp}/{characters[(int)CHARACTER.Monster].maxHp})");
            Console.WriteLine();

            Console.WriteLine($"{characters[(int)CHARACTER.Monster].name}があらわれた！！！");
        }


        static void SelectCommand()
        {
            characters[(int)CHARACTER.Player].command = (int)COMMAND.Fight;

            while (true)
            {
                DrawBattleScreen();
                for (int i = 0; i < (int)COMMAND.MAX; ++i) Console.WriteLine((i == characters[(int)CHARACTER.Player].command ? ">" : "  ") + commandNames[i]);
                switch (Console.ReadLine())
                {
                    case "w":
                        if (--characters[(int)CHARACTER.Player].command == -1) characters[(int)CHARACTER.Player].command = (int)COMMAND.MAX - 1;
                        break;

                    case "s":
                        if (++characters[(int)CHARACTER.Player].command == (int)COMMAND.MAX) characters[(int)CHARACTER.Player].command = 0;
                        break;

                    default:
                        return;
                }

            }
        }
    }
}