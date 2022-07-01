﻿using System;

namespace Tibia
{
    class Program
    {
        static void Main(string[] args)
        {
            var character = new Character();
            Console.WriteLine("Character name: ");
            character.CharacterName = Console.ReadLine();
            Console.WriteLine("Vocation: ");
            character.Vocation = Console.ReadLine();
            Console.WriteLine("Residence: ");
            character.Residence = Console.ReadLine();
            Console.WriteLine("Guild: ");
            character.Guild = Console.ReadLine();
            character.Level = 1;
            character.Experience = 0;
            var characterRepository = new CharacterRepository();
            characterRepository.Add(character);
            var charName = character.CharacterName;

            bool inProgress = true;

            while (inProgress == true)
            {
                string task = Console.ReadLine();

                if (task == "1")
                {
                    characterRepository.GetStats(charName);
                }
                if (task == "2")
                {
                    var characterService = new CharacterService();
                    characterService.KillMonster(charName, "Dog");
                }
                if (task == "3")
                {
                    var characterService = new CharacterService();
                    characterService.KillMonster(charName, "Rotworm");
                }
                if (task == "4")
                {
                    var characterService = new CharacterService();
                    characterService.KillMonster(charName, "Cyclops");
                }
                if (task == "0")
                {
                    Console.WriteLine("Items in backpack:");
                    var backpack = characterRepository.Get(charName).Equipment.Backpack;
                    foreach (var item in backpack)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }
    }
}
