using System;
using System.Diagnostics;
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
        RunAway,
        MAX
    }

    internal class Program
    {
        static Character[] monsters = new Character[(int)MONSTER.MAX];
        static Character[] characters = new Character[(int)CHARACTER.MAX];
        static string[] commandNames = new string[(int)COMMAND.MAX] {
            "たたかう",
            "じゅもん",
            "にげる"
        };
        static Random rng = new Random();
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
                monsters[(int)MONSTER.Slime].attack = 2;
                monsters[(int)MONSTER.Slime].AA = "／・Д・＼\n～～～～～";
            }


            {

            }

            Init();
            Battle((int)MONSTER.Slime);
        }

        static void Init()
        {
            rng = new Random();
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
                for (int i = 0; i < (int)CHARACTER.MAX; ++i)
                {
                    switch (characters[i].command)
                    {
                        case (int)COMMAND.Fight:
                            {
                                Console.WriteLine($"{characters[i].name}の攻撃");

                                Console.ReadKey();

                                int attack = rng.Next(1, characters[i].attack);
                                break;
                            }


                        case (int)COMMAND.Spell:
                            break;

                        case (int)COMMAND.RunAway:
                            break;
                        default:
                            throw new Exception();
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

            Console.WriteLine($"{characters[(int)CHARACTER.Monster].AA}");
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