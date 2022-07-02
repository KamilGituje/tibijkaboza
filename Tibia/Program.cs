using System;

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
            character.MaxCapacity = 500;
            character.CurrentCapacity = character.MaxCapacity;
            character.Equipment.Gold = 0;
            var characterRepository = new CharacterRepository();
            characterRepository.Add(character);
            var charName = character.CharacterName;
            var characterService = new CharacterService();

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
                    characterService.KillMonster(charName, "Dog");
                }
                if (task == "3")
                {
                    characterService.KillMonster(charName, "Rotworm");
                }
                if (task == "4")
                {
                    characterService.KillMonster(charName, "Cyclops");
                }
                if (task == "9")
                {
                    Console.WriteLine("Sell item:");
                    var item = Console.ReadLine();
                    characterService.SellLoot(item, charName);
                }
                if (task == "0")
                {
                    Console.WriteLine("Items in backpack:");
                    var backpack = characterRepository.Get(charName).Equipment.Backpack;
                    foreach (var item in backpack)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine($"Capacity: {Math.Round(character.CurrentCapacity)}");
                    Console.WriteLine($"Gold: {character.Equipment.Gold}");
                }
            }
        }
    }
}
