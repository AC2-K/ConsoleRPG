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
        const int SPELL_COST = 3;
        static Random gen = new Random();
        static Character[] monsters = new Character[(int)MONSTER.MAX];
        static Character[] characters = new Character[(int)CHARACTER.MAX];
        static string[] commandNames = new string[(int)COMMAND.MAX] {
            "たたかう",
            "じゅもん",
            "にげる"
        };
        static void Main(string[] args)
        {
            for (int i = 0; i < monsters.Length; ++i) monsters[i] = new Character();
            for (int i = 0; i < characters.Length; ++i) characters[i] = new Character();
            {
                monsters[(int)MONSTER.Player].hp = 15;
                monsters[(int)MONSTER.Player].maxHp = 15;
                monsters[(int)MONSTER.Player].mp = 15;
                monsters[(int)MONSTER.Player].maxMp = 15;
                monsters[(int)MONSTER.Player].attack = 3;
                monsters[(int)MONSTER.Player].name = "ゆうしゃ";
            }

            {
                monsters[(int)MONSTER.Slime].hp = 3;
                monsters[(int)MONSTER.Slime].maxHp = 3;
                monsters[(int)MONSTER.Slime].mp = 0;
                monsters[(int)MONSTER.Slime].maxMp = 0;
                monsters[(int)MONSTER.Slime].name = "スライム";
                monsters[(int)MONSTER.Slime].attack = 3;
                monsters[(int)MONSTER.Slime].AA = "／・Д・＼\n～～～～～";
            }


            {
                monsters[(int)MONSTER.Boss].hp = 255;
                monsters[(int)MONSTER.Boss].maxHp = 255;
                monsters[(int)MONSTER.Boss].mp = 0;
                monsters[(int)MONSTER.Boss].attack = 50;
                monsters[(int)MONSTER.Boss].maxHp = 0;
                monsters[(int)MONSTER.Boss].name = "魔王";
                monsters[(int)MONSTER.Boss].AA = "    A@A\nΦ （▼Ⅲ▼) Φ";
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

                                int damege = gen.Next(1, characters[i].attack);
                                characters[characters[i].target].hp -= damege;

                                if (characters[characters[i].target].hp<= 0) {
                                    characters[characters[i].target].hp = 0;
                                }
                                DrawBattleScreen();

                                Console.WriteLine($"{characters[characters[i].target].name} に {damege}のダメージ!");
                                Console.ReadKey();
                                break;
                            }


                        case (int)COMMAND.Spell:
                            if (characters[i].mp < SPELL_COST)
                            {
                                Console.WriteLine("MP がたりない！");
                                Console.ReadKey();
                                break;
                            }


                            Console.WriteLine($"{characters[i].name} は ヒール を唱えた");
                            Console.ReadKey();
                            characters[i].mp -= SPELL_COST;
                            characters[i].hp = characters[i].maxHp;
                            DrawBattleScreen();

                            Console.WriteLine($"{characters[i].name} は回復した");
                            Console.ReadKey();
                            break;

                        case (int)COMMAND.Run:
                            Console.WriteLine($"{characters[i].name}は にげだした!!!");
                            Console.ReadKey();
                            return;
                            break;
                        default:
                            throw new Exception();
                    }

                    if (characters[characters[i].target].hp <= 0)
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




    class Character
    { 
        public int hp, maxHp, mp, maxMp, attack;
        public string name;
        public string AA;
        public int command;
        public int target;
    }
}