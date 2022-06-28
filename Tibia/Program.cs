using System;
using System.Collections;
using System.Collections.Generic;

namespace Tibia
{
    class Program
    {
        static void Main(string[] args)
        {
            var characterName = "";
            while(characterName != "end")
            {
                var character = new CharacterRepository();
                Console.WriteLine("Character name: ");
                characterName = Console.ReadLine();
                var command = "";

                while (command != "3" && characterName != "end")
                {
                    command = Console.ReadLine();
                    if (command == "1")
                    {
                        Console.WriteLine(characterName + " Eq:");
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Legs);
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Armor);
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Amulet);
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Helmet);
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Ring);
                        Console.WriteLine(character.Retrieve(characterName).Equipment.Weapon);
                        Console.WriteLine();
                    }
                    if (command == "2")
                    {
                        Console.WriteLine(characterName + " Stats:");
                        Console.WriteLine("Level: " + character.Retrieve(characterName).Level);
                        Console.WriteLine("Vocation: " + character.Retrieve(characterName).Vocation);
                        Console.WriteLine("Guild: " + character.Retrieve(characterName).Guild);
                        Console.WriteLine();
                    }
                }

            }
        }
    }
}
